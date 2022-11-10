using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected AbilitySlot Slot { get; private set; }
    protected ITargetSource TargetSource { get; private set; }

    public void Init(AbilitySlot slot, ITargetSource targetSource)
    {
        Slot = slot;
        TargetSource = targetSource;
    }

    public virtual void Use() { }
}