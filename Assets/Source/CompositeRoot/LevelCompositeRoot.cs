using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CompositeRoot
{
    public class LevelCompositeRoot : CompositeRoot
    {
        [SerializeField] private Config _config;
        [SerializeField] private ProgressBarView _levelProgressView;
        [SerializeField] private TextView _currentLevelView;
        [SerializeField] private WinWndow _winWindow;
        [SerializeField] private FailWindow _failWindow;
        [SerializeField] private CharacterCompositeRoot _characterCompositeRoot;
        [SerializeField] private BotSpawner _botSpawner;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private BotObjectPool _botObjectPool;
        [SerializeField] private RewardObjectPool _rewardsObjectPool;
        [SerializeField] private float _waitTimeAfterLevelComplete;
        [SerializeField] private Saver _save;
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private UnityEvent _onStartGame;

        public int EnemyAmount { get; private set; }

        public event UnityAction GameStarted
        {
            add => _onStartGame.AddListener(value);
            remove => _onStartGame.RemoveListener(value);
        }

        public Counter DeathCounter { get; private set; }

        public override void Compose()
        {
            EnemyAmount = (int)_config.WaveSpawnerAmount.Evaluate(_sceneLoader.CurrentSceneIndex);
            DeathCounter = new Counter(EnemyAmount);
            _botObjectPool.Init();
            _rewardsObjectPool.Init();
            AudioListener.pause = _save.PlayerData.IsSoundMute;
        }

        private void OnEnable()
        {
            DeathCounter.Changed += OnDeathCounterChanged;
            DeathCounter.Complited += OnDeathCounterComplited;
            _characterCompositeRoot.Character.Health.Died += OnCharacterDied;
        }

        private void OnDisable()
        {
            DeathCounter.Changed -= OnDeathCounterChanged;
            DeathCounter.Complited -= OnDeathCounterComplited;
            _characterCompositeRoot.Character.Health.Died -= OnCharacterDied;
        }

        private void Start()
        {
            _leaderboardView.Show();
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
            _save.SaveLevel(_sceneLoader.NextScene);
            _save.PlayerData.IsSoundMute = AudioListener.pause;
            _characterCompositeRoot.Save();
            _winWindow.Open(rewardGold, () =>
            {
                Resume();
                _sceneLoader.LoadNext();
            });

#if UNITY_WEBGL && !UNITY_EDITOR
            Leaderboard.SetScore("Leaderboard", (int)_characterCompositeRoot.Character.Level.Exp);
#endif
        }

        private void GameOver()
        {
            Pause();
            _failWindow.Open((resurrected) => 
            {
                if (resurrected)
                {
                    Resume();
                }
                else
                {
                    Resume();
                    _sceneLoader.Restart();
                }
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
            _characterCompositeRoot.AbilityFactory.Create();
            _characterCompositeRoot.AbilityUpgrade.Init(_characterCompositeRoot.Character);
            Resume();
            _onStartGame?.Invoke();
        }
    }
}