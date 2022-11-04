using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract IEnumerable<State> BaseStates { get; }
    protected Slot Slot { get; private set; }
    protected ITargetSource TargetSource { get; private set; }

    public void Init(Slot slot, ITargetSource targetSource)
    {
        Slot = slot;
        TargetSource = targetSource;
    }
}