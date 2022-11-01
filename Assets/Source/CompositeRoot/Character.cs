using UnityEngine;

public class Character : CompositeRoot
{
    [SerializeField] private Config _config;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private TextView _levelPresenter;
    [SerializeField] private AbilityUpgrade _abilityUpgrade;
    [SerializeField] private LevelCompositeRoot _levelComposite;
    [SerializeField] private PlayerTouchInputView _playerTouchInputView;

    public PlayerInput Input { get; private set; }
    public Health Health { get; private set; }
    public CharacterLevel CharacterLevel { get; private set; }
    public Wallet Wallet { get; private set; }

    public override void Compose()
    {
        Health = new Health(_config.CharacterHealth);
        CharacterLevel = new CharacterLevel(0, 0, _config.CharacterLevelUp);
        Input = new PlayerInput();
        Wallet = new Wallet();
        _playerTouchInputView.Init(Input);
        _movement.Init(Input);
    }

    private void OnEnable()
    {
        Health.Changed += _healthView.Render;
        CharacterLevel.LevelChanged += OnCharacterLevelChanged;
        Input.Enable();
    }

    private void OnDisable()
    {
        Health.Changed -= _healthView.Render;
        CharacterLevel.LevelChanged -= OnCharacterLevelChanged;
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