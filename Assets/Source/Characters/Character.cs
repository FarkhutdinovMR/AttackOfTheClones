using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public AbilityUpgrade AbilityUpgrade { get; private set; }
    [SerializeField] public PlayerTouchInputView _touchInputView;
    [SerializeField] public AbilityFactory _abilityFactory;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private TextView _levelPresenter;

    [SerializeField] private MonoBehaviour _healthViewSource;
    private IHealthView _healthView => (IHealthView)_healthViewSource;

    [SerializeField] private MonoBehaviour _walletViewSource;
    public IWalletView _walletView => (IWalletView)_walletViewSource;

    [field: SerializeField] public State[] States { get; private set; }
    [field: SerializeField] public Inventory Inventory { get; private set; }
    public Health Health { get; private set; }
    public CharacterLevel Level { get; private set; }
    public Wallet Wallet { get; private set; }

    public PlayerInput Input { get; private set; }

    public void Init(IGame game, Config config, Saver.Data data)
    {
        Health = new CharacterHealth(config.CharacterHealth, _healthView, game);
        Level = data.CharacterLevel;
        Level.SetGame(game);
        Wallet = data.Wallet;
        Inventory = data.Inventory;
        States = data.CharacterState;
        Input = new PlayerInput();
        _movement.Init(Input);
        _touchInputView.Init(Input);
        enabled = true;
    }

    private void OnValidate()
    {
        if (_healthViewSource && !(_healthViewSource is IHealthView))
        {
            Debug.LogError(_healthViewSource + " not implement " + nameof(IHealthView));
            _healthViewSource = null;
        }

        if (_walletViewSource && !(_walletViewSource is IWalletView))
        {
            Debug.LogError(_walletViewSource + " not implement " + nameof(IWalletView));
            _walletViewSource = null;
        }
    }

    private void OnEnable()
    {
        Input.Enable();
    }

    private void OnDisable()
    {
        Input.Disable();
    }

    private void Start()
    {
        Wallet.ShowGold(_walletView);
        _levelPresenter.Render((int)Level.Value);
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

    public void DisableInputView()
    {
        _touchInputView.gameObject.SetActive(false);
    }

    public void EnableInputView()
    {
        _touchInputView.gameObject.SetActive(true);
    }
}