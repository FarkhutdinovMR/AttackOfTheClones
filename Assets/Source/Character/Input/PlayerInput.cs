using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInput : ICharacterInputSource
{
    private readonly CharacterInput _input;

    private bool _isTouchScreen;
    private Vector2 _startPosition;

    public int TouchScreenSize { get; private set; } = 150;

    public PlayerInput()
    {
        _input = new CharacterInput();
    }

    public Vector2 MovementInput { get; private set; }

    public event Action<Vector2> FingerDowned;
    public event Action<Vector2> FingerMoved;
    public event Action FingerUped;
    public event Action Disabled;

    public void Enable()
    {
        _input.Enable();
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnTouchFingerDown;
        ETouch.Touch.onFingerMove += OnTouchFingerMove;
        ETouch.Touch.onFingerUp += OnTouchFingerUp;
    }

    public void Disable()
    {
        if (EnhancedTouchSupport.enabled)
        {
            ETouch.Touch.onFingerDown -= OnTouchFingerDown;
            ETouch.Touch.onFingerMove -= OnTouchFingerMove;
            ETouch.Touch.onFingerUp -= OnTouchFingerUp;
            EnhancedTouchSupport.Disable();
        }

        _input.Disable();
        MovementInput = Vector2.zero;
        Disabled?.Invoke();
    }

    public void Update()
    {
        if (_isTouchScreen == false)
            MovementInput = _input.Movement.Move.ReadValue<Vector2>();
    }

    private void OnTouchFingerDown(Finger finger)
    {
        if (_isTouchScreen)
            return;

        _isTouchScreen = true;
        int x = (int)Math.Clamp(finger.screenPosition.x, TouchScreenSize, Screen.width - TouchScreenSize);
        int y = (int)Math.Clamp(finger.screenPosition.y, TouchScreenSize, Screen.height - TouchScreenSize);
        _startPosition = new Vector2(x, y);
        FingerDowned?.Invoke(_startPosition);
    }

    private void OnTouchFingerMove(Finger finger)
    {
        Vector2 delta = finger.screenPosition - _startPosition;
        delta /= TouchScreenSize;

        if (delta.magnitude >= 1f)
            delta.Normalize();

        MovementInput = delta;
        FingerMoved?.Invoke(delta);
    }

    private void OnTouchFingerUp(Finger finger)
    {
        if (_isTouchScreen == false)
            return;

        MovementInput = Vector2.zero;
        _isTouchScreen = false;
        FingerUped?.Invoke();
    }
}