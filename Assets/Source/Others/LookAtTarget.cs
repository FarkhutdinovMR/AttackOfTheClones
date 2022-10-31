using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        if (_target == null)
            return;

        transform.rotation = Quaternion.LookRotation(transform.position - _target.position);
    }
}