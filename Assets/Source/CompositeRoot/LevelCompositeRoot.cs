using UnityEngine;

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
        }

        private void OnDeathCounterChanged(int count)
        {
            _levelProgressView.Render((float)count / _spawner.Amount);
        }

        private void OnDeathCounterComplited()
        {
            Invoke(nameof(CompleteLevel), _waitTimeAfterLevelComplete);
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            _characterCompositeRoot.Input.Disable();
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            _characterCompositeRoot.Input.Enable();
        }

        private void CompleteLevel()
        {
            Pause();
            _characterCompositeRoot.Character.Wallet.Add(_characterCompositeRoot.Character.Level.Exp);
            _winWindow.Open(_characterCompositeRoot.Character.Wallet.Gold, Resume);
        }

        private void OnCharacterDied()
        {
            Pause();
            _failWindow.Open(Resume);
        }
    }
}