using UnityEngine;

public class Ability : MonoBehaviour
{
    protected Slot Slot { get; private set; }
    protected ITargetSource TargetSource { get; private set; }

    public void Init(Slot slot, ITargetSource targetSource)
    {
        Slot = slot;
        TargetSource = targetSource;
    }

    public virtual void Use() { }
}