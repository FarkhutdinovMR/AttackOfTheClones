using UnityEngine;

namespace CompositeRoot
{
    public class CharacterCompositeRoot : CompositeRoot
    {
        [field: SerializeField] public Character Character { get; private set; }
        [field: SerializeField] public PlayerTouchInputView PlayerTouchInputView { get; private set; }
        [field: SerializeField] public AbilityUpgrade AbilityUpgrade { get; private set; }
        [SerializeField] public AbilityFactory _abilityFactory;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private Config _config;
        [SerializeField] private Saver _save;
        [SerializeField] private TextView _levelPresenter;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private TextView _goldView;

        public PlayerInput Input { get; private set; }

        public override void Compose()
        {
            _save.Load();
            Character.Init(_config, _save.PlayerData);
            Input = new PlayerInput();
            _movement.Init(Input);
            PlayerTouchInputView.Init(Input);
        }

        private void OnEnable()
        {
            Character.Health.Changed += _healthView.Render;
            Character.Wallet.Changed += _goldView.Render;
            Input.Enable();
        }

        private void OnDisable()
        {
            Character.Health.Changed -= _healthView.Render;
            Character.Wallet.Changed -= _goldView.Render;
            Input.Disable();
        }

        private void Start()
        {
            _goldView.Render(Character.Wallet.Gold);
            _levelPresenter.Render((int)Character.Level.Value);
        }

        private void Update()
        {
            Input.Update();
        }

        public void CreateAbilities()
        {
            _abilityFactory.Create();
            AbilityUpgrade.Init();
        }
    }
}