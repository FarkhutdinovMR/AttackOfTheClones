using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        if (_target == null)
            return;

        transform.position = _target.position;
    }
}