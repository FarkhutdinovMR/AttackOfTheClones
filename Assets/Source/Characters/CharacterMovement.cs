using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private ICharacterInputSource _inputSource => (ICharacterInputSource)_inputSourceBehaviour;
    private CharacterController characterController;
    private Transform _lookTarget;

    public float CurrentSpeed => characterController.velocity.magnitude;

    private void OnValidate()
    {
        if (_inputSourceBehaviour && !(_inputSourceBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _inputSourceBehaviour = null;
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move(_inputSource.MovementInput);
        LookAt();
    }

    public void SetTarget(Transform target)
    {
        _lookTarget = target;
    }

    private void Move(Vector2 delta)
    {
        var direction = new Vector3(delta.x, 0f, delta.y);
        direction *= _moveSpeed;
        characterController.SimpleMove(direction);
    }

    private void LookAt()
    {
        Vector3 direction;

        if (_lookTarget != null)
            direction = _lookTarget.position - transform.position;
        else
            direction = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);

        if (direction == Vector3.zero)
            return;

        direction = new Vector3(direction.x, 0f, direction.z);
        Quaternion rotate = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _rotateSpeed * Time.deltaTime);
    }
}