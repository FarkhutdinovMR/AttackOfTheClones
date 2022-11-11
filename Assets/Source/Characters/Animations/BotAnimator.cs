using UnityEngine;

[RequireComponent (typeof(Animator))]
public class BotAnimator : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private float _dampTime = 0.1f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.SetFloat(AnimatorCharacterController.Params.Offset, Random.Range(0f, 1f));
    }

    private void Update()
    {
        _animator.SetFloat(AnimatorCharacterController.Params.Forward, _movement.CurrentSpeed, _dampTime, Time.deltaTime);
    }
}