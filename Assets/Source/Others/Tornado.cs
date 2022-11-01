using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField] private Transform _eccentric;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TornadoInteractable tornadoInteractable))
            other.transform.parent = _eccentric;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TornadoInteractable tornadoInteractable))
            other.transform.parent = null;
    }
}