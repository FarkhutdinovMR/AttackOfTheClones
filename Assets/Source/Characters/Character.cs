using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public State[] States { get; private set; }

    public Health Health { get; private set; }
    public CharacterLevel Level { get; private set; }
    public Wallet Wallet { get; private set; }

    public void Init(Config config)
    {
        Health = new Health(config.CharacterHealth);
        Level = new CharacterLevel(0, 0, config.CharacterLevelUp);
        Wallet = new Wallet();
    }
}