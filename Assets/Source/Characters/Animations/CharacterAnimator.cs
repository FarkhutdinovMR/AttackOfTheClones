using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private float _dampTime = 1f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 direction = _movement.Direction;
        direction = transform.InverseTransformDirection(direction);
        float right = direction.x;
        float forward = direction.z;

        _animator.SetFloat(AnimatorCharacterController.Params.Right, right, _dampTime, Time.deltaTime);
        _animator.SetFloat(AnimatorCharacterController.Params.Forward, forward, _dampTime, Time.deltaTime);
    }
}