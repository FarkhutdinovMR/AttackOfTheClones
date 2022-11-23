using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class Root : MonoBehaviour, IGame
{
    [SerializeField] private Config _config;
    [SerializeField] private Character _character;
    [SerializeField] private Enemies _enemies;
    [SerializeField] private StoreUI _storeUI;
    [SerializeField] private TextView _currentLevelView;
    [SerializeField] private WinWndow _winWindow;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private LeaderboardView _leaderboardView;
    [SerializeField] private Audio _audio;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Language _language;
    [SerializeField] private float _waitTimeAfterLevelComplete;

    private SceneLoader _sceneLoader;
    private Saver _saver;

    private void Awake()
    {
        _sceneLoader = new SceneLoader();
        _saver = new PlayerPrefsJSONSaver(_config, _character);
        _character.Init(this, _config, _saver.Load());
        _enemies.Init(_character, _config, _sceneLoader.CurrentSceneIndex, this);
        _storeUI.Init(new Store(_character.Inventory, _character.Wallet), _character);
        _audio.Init(Convert.ToUInt32(_saver.PlayerData.IsMute));

#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(WaitSDKInitialize(()=>
        {
            _language.Set(YandexGamesSdk.Environment.i18n.lang);
            _leaderboardView.Show();
        }));
#endif

        Pause();
        StartCoroutine(WaitMoveInput(StartGame));
    }

    private void Start()
    {
        _currentLevelView.Render(_sceneLoader.CurrentSceneIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _character.DisableInputView();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _character.EnableInputView();
    }

    public void CompleteLevel()
    {
        StartCoroutine(Wait(() =>
        {
            Pause();
            uint rewardGold = _enemies.DeathCounter.Value / 2;
            _character.Wallet.Add(rewardGold);
            Save();
            _winWindow.Open(rewardGold, () => _sceneLoader.LoadNext());
            SetLeaderboardScore((int)_character.Score.Value);
        }));
    }

    public void GameOver()
    {
        Pause();
        _failWindow.Open((resurrected) => 
        {
            if (resurrected)
                Resume();
            else
                _sceneLoader.Restart();
        });
    }

    public void UpgradeCharacter()
    {
        Pause();
        _character.UpgradeAbility(Resume);
    }

    private void StartGame()
    {
        _character.CreateAbilities();
        _mainMenu.HideStartingMenu();
        Resume();
    }

    private IEnumerator WaitMoveInput(Action onCharacterMoveCallback)
    {
        while(true)
        {
            yield return null;
            if (_character.Input.MovementInput == Vector2.zero)
                continue;

            onCharacterMoveCallback();
            yield break;
        }
    }

    private IEnumerator WaitSDKInitialize(Action onSDKInitilized)
    {
        while (true)
        {
            if (YandexGamesSdk.IsInitialized)
            {
                onSDKInitilized();
                yield break;
            }

            yield return new WaitForSecondsRealtime(1);
        }
    }

    private IEnumerator Wait(Action onEndCallback)
    {
        yield return new WaitForSecondsRealtime(_waitTimeAfterLevelComplete);
        onEndCallback?.Invoke();
    }

    private void SetLeaderboardScore(int value)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (YandexGamesSdk.IsInitialized == false)
            return;
            
        Leaderboard.SetScore(LeaderboardView.LeaderboardName, value);
#endif
    }

    private void Save()
    {
        _saver.PlayerData.NextLevel = _sceneLoader.NextSceneIndex;
        _saver.PlayerData.IsMute = _audio.Pause;
        _saver.PlayerData.Level = (CharacterLevel)_character.Level;
        _saver.PlayerData.Experience = (Experience)_character.Experience;
        _saver.PlayerData.Score = (CharacterScore)_character.Score;
        _saver.Save();
    }
}