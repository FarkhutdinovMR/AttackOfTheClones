using UnityEngine;

namespace CompositeRoot
{
    public class CharacterCompositeRoot : CompositeRoot
    {
        [field: SerializeField] public Character Character { get; private set; }
        [field: SerializeField] public AbilityFactory AbilityFactory { get; private set; }
        [field: SerializeField] public PlayerTouchInputView PlayerTouchInputView { get; private set; }
        [field: SerializeField] public AbilityUpgrade AbilityUpgrade { get; private set; }
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private Config _config;
        [SerializeField] private Character _characterTemplate;
        [SerializeField] private LevelCompositeRoot _levelComposite;
        [SerializeField] private TextView _levelPresenter;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private Saver _save;
        [SerializeField] private TextView _goldView;
        [SerializeField] private TextView _levelPresenter2;
        [SerializeField] private Store _store;
        [SerializeField] private StoreView _storeView;
        [SerializeField] private AbilityInventoryView _abilityInventoryView;

        public PlayerInput Input { get; private set; }

        public override void Compose()
        {
            Saver.Data data = _save.Load();
            Character.Init(_config, data);
            Input = new PlayerInput();
            _movement.Init(Input);
            PlayerTouchInputView.Init(Input);
        }

        private void OnEnable()
        {
            Character.Health.Changed += _healthView.Render;
            Character.Level.LevelChanged += OnCharacterLevelChanged;
            Character.Wallet.Changed += _goldView.Render;
            Input.Enable();
        }

        private void OnDisable()
        {
            Character.Health.Changed -= _healthView.Render;
            Character.Level.LevelChanged -= OnCharacterLevelChanged;
            Character.Wallet.Changed -= _goldView.Render;
            Input.Disable();
        }

        private void Start()
        {
            Character.enabled = true;
            _goldView.Render(Character.Wallet.Gold);
            _levelPresenter2.Render((int)Character.Level.Value);
        }

        private void Update()
        {
            Input.Update();
        }

        public void Save()
        {
            _save.Save();
        }

        private void OnCharacterLevelChanged(uint level)
        {
            _levelComposite.Pause();
            AbilityUpgrade.OpenUpgradeWindow(_levelComposite.Resume);
            _levelPresenter.Render((int)level);
            _levelPresenter2.Render((int)level);
        }
    }
}