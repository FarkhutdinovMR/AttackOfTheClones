using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CompositeRoot
{
    public class LevelCompositeRoot : CompositeRoot
    {
        [SerializeField] private WaveSpawner _spawner;
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
        [SerializeField] private PlayerPrefsSaver _save;
        [SerializeField] private UnityEvent _onStartGame;

        public event UnityAction GameStarted
        {
            add => _onStartGame.AddListener(value);
            remove => _onStartGame.RemoveListener(value);
        }

        public Counter DeathCounter { get; private set; }

        public override void Compose()
        {
            DeathCounter = new Counter(_spawner.Amount);
            _botObjectPool.Init();
            _rewardsObjectPool.Init();
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
            _currentLevelView.Render(_sceneLoader.CurrentSceneIndex);
            Pause();
            StartCoroutine(WaitMoveInput(StartGame));
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
            _levelProgressView.Render((float)count / _spawner.Amount);
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
            uint rewardGold = DeathCounter.Value;
            _characterCompositeRoot.Character.Wallet.Add(rewardGold);
            _characterCompositeRoot.Save();
            _winWindow.Open(rewardGold, () =>
            {
                Resume();
                _sceneLoader.LoadNext();
            });
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
            _onStartGame?.Invoke();
            _characterCompositeRoot.AbilityFactory.UpdateSlots();
            Resume();
        }
    }
}