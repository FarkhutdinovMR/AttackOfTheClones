using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _targetSourceBehaviour;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private ICharacterInputSource _inputSource;
    private CharacterController _characterController;
    private Transform _lookTarget;

    private ITargetSource _targetSource => (ITargetSource)_targetSourceBehaviour;
    public float CurrentSpeed => _characterController.velocity.magnitude;
    public Vector3 Direction => _characterController.velocity;

    public void Init(ICharacterInputSource inputSource)
    {
        _inputSource = inputSource;
        enabled = true;
    }

    private void OnValidate()
    {
        if (_targetSourceBehaviour && !(_targetSourceBehaviour is ITargetSource))
        {
            Debug.LogError(nameof(_targetSourceBehaviour) + " needs to implement " + nameof(ITargetSource));
            _targetSourceBehaviour = null;
        }
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move(_inputSource.MovementInput);
        LookAt();
    }

    private void Move(Vector2 delta)
    {
        var direction = new Vector3(delta.x, 0f, delta.y);
        direction *= _moveSpeed;
        _characterController.SimpleMove(direction);
    }

    private void LookAt()
    {
        Vector3 direction;

        if (_targetSource != null && _targetSource.Target != null)
            direction = _targetSource.Target.position - transform.position;
        else
            direction = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);

        if (direction == Vector3.zero)
            return;

        direction = new Vector3(direction.x, 0f, direction.z);
        Quaternion rotate = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _rotateSpeed * Time.deltaTime);
    }
}