using UnityEngine;

public class Ability : MonoBehaviour
{
    [field: SerializeField] public State[] States { get; private set; }

    protected Slot Slot { get; private set; }
    protected ITargetSource TargetSource { get; private set; }

    public void Init(Slot slot, ITargetSource targetSource)
    {
        Slot = slot;
        TargetSource = targetSource;
    }
}