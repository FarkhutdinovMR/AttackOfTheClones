using UnityEngine;

[RequireComponent (typeof(Animator))]
public class BotAnimator : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(AnimatorCharacterController.Params.Forward, _movement.CurrentSpeed);
    }
}