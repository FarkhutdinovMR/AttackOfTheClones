using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private TextPresenter _levelPresenter;
    [SerializeField] private AbilityUpgrade _abilityUpgrade;
    [SerializeField] private LevelCompositeRoot _levelComposite;

    private PlayerInput _playerInput;

    public Health Health { get; private set; }
    public CharacterLevel CharacterLevel { get; private set; }

    private void Awake()
    {
        Health = new Health(_config.CharacterHealth);
        CharacterLevel = new CharacterLevel(0, 0, _config.CharacterLevelUp);
        _playerInput = new PlayerInput();
        _movement.Init(_playerInput);
    }

    private void OnEnable()
    {
        Health.Changed += _healthView.Render;
        CharacterLevel.LevelChanged += OnCharacterLevelChanged;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        Health.Changed -= _healthView.Render;
        CharacterLevel.LevelChanged -= OnCharacterLevelChanged;
        _playerInput.Disable();
    }

    private void Update()
    {
        _playerInput.Update();
    }

    private void OnCharacterLevelChanged(uint level)
    {
        _levelComposite.Pause();
        _abilityUpgrade.OpenUpgradeWindow(_levelComposite.Resume);
        _levelPresenter.Render(level);
    }
}