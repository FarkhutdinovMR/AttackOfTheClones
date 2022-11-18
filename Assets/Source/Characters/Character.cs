using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public State[] States { get; private set; }
    [field: SerializeField] public Inventory Inventory { get; private set; }
    public Health Health { get; private set; }
    public CharacterLevel Level { get; private set; }
    public Wallet Wallet { get; private set; }

    public void Init(Config config, Saver.Data data)
    {
        Health = new Health(config.CharacterHealth);
        Level = data.CharacterLevel;
        Wallet = data.Wallet;
        Inventory = data.Inventory;
        States = data.CharacterState;
    }
}