using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private FireballAttackDamage _state;
    [SerializeField] private float _time;

    private Slot _slot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AbilityFactory abilityFactory))
        {
            _slot = abilityFactory.Slots[1];
            //_slot.AddState(_state);
            Invoke(nameof(Disable), _time);
            gameObject.SetActive(false);
        }
    }

    private void Disable()
    {
        //_slot.RemoveState(_state);
    }
}