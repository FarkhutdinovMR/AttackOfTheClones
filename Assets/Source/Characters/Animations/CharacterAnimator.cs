using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(AnimatorCharacterController.Params.Speed, _movement.CurrentSpeed);
    }
}