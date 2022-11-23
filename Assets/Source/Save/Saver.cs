using System;

public abstract partial class Saver
{
    private readonly Config _config;
    private readonly Character _character;

    protected Saver(Config config, Character character)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _character = character ?? throw new ArgumentNullException(nameof(character));
    }

    public Data PlayerData { get; protected set; }

    public abstract void Save();

    protected abstract bool TryLoad();

    public virtual Data Load()
    {
        if (TryLoad() == false)
            SetDefaultData();

        return PlayerData;
    }

    private void SetDefaultData()
    {
        PlayerData = new Data()
        {
            Wallet = new Wallet(_config.CharacterStartGold),
            CharacterState = _character.States,
            Inventory = _character.Inventory,
            NextLevel = _config.FirstLevelIndex,
            IsMute = false
        };

        PlayerData.Inventory.Slots[0].Equip(PlayerData.Inventory.Abilities[0]);
    }
}