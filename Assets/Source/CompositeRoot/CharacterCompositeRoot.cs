using UnityEngine;

namespace CompositeRoot
{
    public class CharacterCompositeRoot : CompositeRoot
    {
        [field: SerializeField] public Character Character { get; private set; }
        [field: SerializeField] public AbilityFactory AbilityFactory { get; private set; }
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private Config _config;
        [SerializeField] private Character _characterTemplate;
        [SerializeField] private AbilityUpgrade _abilityUpgrade;
        [SerializeField] private LevelCompositeRoot _levelComposite;
        [SerializeField] private TextView _levelPresenter;
        [SerializeField] private PlayerTouchInputView _playerTouchInputView;
        [SerializeField] private HealthView _healthView;

        public PlayerInput Input { get; private set; }

        public override void Compose()
        {
            Character.Init(_config);
            Input = new PlayerInput();
            _movement.Init(Input);
            _playerTouchInputView.Init(Input);
            _abilityUpgrade.Init(Character.States, AbilityFactory);
        }

        private void OnEnable()
        {
            Character.Health.Changed += _healthView.Render;
            Character.Level.LevelChanged += OnCharacterLevelChanged;
            Input.Enable();
        }

        private void OnDisable()
        {
            Character.Health.Changed -= _healthView.Render;
            Character.Level.LevelChanged -= OnCharacterLevelChanged;
            Input.Disable();
        }

        private void Update()
        {
            Input.Update();
        }

        private void OnCharacterLevelChanged(uint level)
        {
            _levelComposite.Pause();
            _abilityUpgrade.OpenUpgradeWindow(_levelComposite.Resume);
            _levelPresenter.Render((int)level);
        }
    }
}