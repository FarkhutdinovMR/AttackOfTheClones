using Agava.YandexGames;
using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CompositeRoot
{
    public class LevelCompositeRoot : CompositeRoot
    {
        [SerializeField] private CharacterCompositeRoot _characterCompositeRoot;
        [SerializeField] private Config _config;
        [SerializeField] private ProgressBarView _levelProgressView;
        [SerializeField] private TextView _currentLevelView;
        [SerializeField] private WinWndow _winWindow;
        [SerializeField] private FailWindow _failWindow;
        [SerializeField] private BotSpawner _botSpawner;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private BotObjectPool _botObjectPool;
        [SerializeField] private RewardObjectPool _rewardsObjectPool;
        [SerializeField] private Saver _save;
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private Audio _audio;
        [SerializeField] private float _waitTimeAfterLevelComplete;
        [SerializeField] private Animator _menuAnimator;

        private Dictionary<string, string> _languageISO639_1Codes = new()
        {
            { "ru", "Russian" },
            { "en", "English" },
            { "tr", "Turkish" },
        };

        private const string HideMenuAnimation = "HideMenuAnimation";

        public int EnemyAmount { get; private set; }
        public Counter DeathCounter { get; private set; }

        public override void Compose()
        {
            EnemyAmount = (int)_config.WaveSpawnerAmount.Evaluate(_sceneLoader.CurrentSceneIndex);
            DeathCounter = new Counter(EnemyAmount);
            _botObjectPool.Init();
            _rewardsObjectPool.Init();
            _audio.Init(Convert.ToUInt32(_save.PlayerData.IsMute));

#if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(WaitSDKInitialize(()=>
            {
                DefineLanguage();
                _leaderboardView.Show();
            }));
#endif
        }

        private void OnEnable()
        {
            DeathCounter.Changed += OnDeathCounterChanged;
            DeathCounter.Complited += OnDeathCounterComplited;
            _characterCompositeRoot.Character.Health.Died += OnCharacterDied;
            _characterCompositeRoot.Character.Level.LevelChanged += OnCharacterLevelChanged;
        }

        private void OnDisable()
        {
            DeathCounter.Changed -= OnDeathCounterChanged;
            DeathCounter.Complited -= OnDeathCounterComplited;
            _characterCompositeRoot.Character.Health.Died -= OnCharacterDied;
            _characterCompositeRoot.Character.Level.LevelChanged -= OnCharacterLevelChanged;
        }

        private void Start()
        {
            _currentLevelView.Render(_sceneLoader.CurrentSceneIndex);
            StartCoroutine(WaitMoveInput(StartGame));
            Pause();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            _characterCompositeRoot.PlayerTouchInputView.gameObject.SetActive(false);
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            _characterCompositeRoot.PlayerTouchInputView.gameObject.SetActive(true);
        }

        private void OnDeathCounterChanged(uint count)
        {
            float value = Convert.ToSingle(count) / EnemyAmount;
            _levelProgressView.Render(value);
        }

        private void OnDeathCounterComplited()
        {
            Invoke(nameof(CompleteLevel), _waitTimeAfterLevelComplete);
        }

        private void OnCharacterDied()
        {
            GameOver();
        }

        private void CompleteLevel()
        {
            Pause();
            uint rewardGold = DeathCounter.Value / 2;
            _characterCompositeRoot.Character.Wallet.Add(rewardGold);
            Save();
            _winWindow.Open(rewardGold, () => _sceneLoader.LoadNext());

#if UNITY_WEBGL && !UNITY_EDITOR
            if (YandexGamesSdk.IsInitialized)
                Leaderboard.SetScore("Leaderboard", (int)_characterCompositeRoot.Character.Level.Score);
#endif
        }

        private void Save()
        {
            _save.SaveLevel(_sceneLoader.NextScene);
            _save.PlayerData.IsMute = _audio.Pause;
            _save.Save();
        }

        private void GameOver()
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

        private IEnumerator WaitMoveInput(Action onCharacterMoveCallback)
        {
            while(true)
            {
                yield return null;
                if (_characterCompositeRoot.Input.MovementInput == Vector2.zero)
                    continue;

                onCharacterMoveCallback();
                yield break;
            }
        }

        private void StartGame()
        {
            _characterCompositeRoot.CreateAbilities();
            _menuAnimator.Play(HideMenuAnimation);
            Resume();
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

        private void DefineLanguage()
        {
            string key = YandexGamesSdk.Environment.i18n.lang;
            if (_languageISO639_1Codes.ContainsKey(key))
                _leanLocalization.SetCurrentLanguage(_languageISO639_1Codes[key]);
        }

        private void OnCharacterLevelChanged(uint level)
        {
            Pause();
            _characterCompositeRoot.AbilityUpgrade.OpenUpgradeWindow(Resume);
        }
    }
}