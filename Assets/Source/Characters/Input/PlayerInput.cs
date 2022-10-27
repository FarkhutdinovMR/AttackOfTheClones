using UnityEngine;

public class PlayerInput : ICharacterInputSource
{
    private CharacterInput _input;

    public PlayerInput()
    {
        _input = new CharacterInput();
    }

    public Vector2 MovementInput { get; private set; }

    public void Enable()
    {
        _input.Enable();
    }

    public void Disable()
    {
        _input.Disable();
    }

    public void Update()
    {
        MovementInput = _input.Movement.Move.ReadValue<Vector2>();
        MovementInput.Normalize();
    }
}