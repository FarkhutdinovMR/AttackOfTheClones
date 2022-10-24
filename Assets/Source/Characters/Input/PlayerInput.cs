using UnityEngine;

public class PlayerInput : MonoBehaviour, ICharacterInputSource
{
    private CharacterInput _input;

    public Vector2 MovementInput { get; private set; }

    private void OnEnable()
    {
        _input = new CharacterInput();
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        MovementInput = _input.Movement.Move.ReadValue<Vector2>();
        MovementInput.Normalize();
    }
}