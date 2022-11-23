using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private AbilityFactory _abilityFactory;
    [SerializeField] private AbilityUpgrade _abilityUpgrade;
    [SerializeField] private PlayerTouchInputView _touchInputView;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private TextView _levelPresenter;
    [SerializeField] private MonoBehaviour _healthViewSource;
    private IHealthView _healthView => (IHealthView)_healthViewSource;

    [SerializeField] private MonoBehaviour _walletViewSource;
    public IWalletView _walletView => (IWalletView)_walletViewSource;

    [field: SerializeField] public State[] States { get; private set; }
    [field: SerializeField] public Inventory Inventory { get; private set; }
    public PlayerInput Input { get; private set; }
    public IHealth Health { get; private set; }
    public IWallet Wallet { get; private set; }
    public ILevel Level { get; private set; }
    public IExperience Experience { get; private set; }
    public IScore Score { get; private set; }

    public void Init(IGame game, Config config, Data data)
    {
        Health = new CharacterHealth(config.CharacterHealth, _healthView, game);
        Level = new CharacterLevel(data.Level == null ? 1 : data.Level.Value, 1000, game);
        Experience = new Experience(Level, config.CharacterLevelProgress, data.Experience == null ? 0 : data.Experience.Value, data.Experience == null ? config.CharacterLevelUpCost : data.Experience.LevelUpCost);
        Score = new CharacterScore(data.Score == null ? 0 : data.Score.Value, Experience);
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
        _abilityUpgrade.Init();
    }

    public void DisableInputView()
    {
        _touchInputView.gameObject.SetActive(false);
    }

    public void EnableInputView()
    {
        _touchInputView.gameObject.SetActive(true);
    }

    public void UpgradeAbility(Action onEndCallback)
    {
        _abilityUpgrade.OpenUpgradeWindow(onEndCallback);
    }
}