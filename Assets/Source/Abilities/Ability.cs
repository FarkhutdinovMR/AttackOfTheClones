using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [field: SerializeField] public List<StateConfig> StateConfigs { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    public List<State> States { get; private set; }
    protected Slot Slot { get; private set; }
    protected ITargetSource TargetSource { get; private set; }

    public void Init(Slot slot, ITargetSource targetSource, Saver.Data data)
    {
        States = new();
        Slot = slot;
        TargetSource = targetSource;
        Init(data);
    }

    protected abstract void Init(Saver.Data data);
}