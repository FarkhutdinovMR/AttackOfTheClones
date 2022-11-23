using System;
using UnityEngine;

public class PlayerTouchInputView : MonoBehaviour
{
    [SerializeField] private RectTransform _joystick;
    [SerializeField] private RectTransform _joystickOutCircle;
    [SerializeField] private RectTransform _joystickPoint;

    private PlayerInput _input;

    public int HalfTouchScreenSize => (int)(_input.TouchScreenSize * 0.5f);

    public void Init(PlayerInput input)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        enabled = true;
    }

    private void OnEnable()
    {
        _input.FingerDowned += OnInputFingerDowned;
        _input.FingerMoved += OnInputFingerMoved;
        _input.FingerUped += OnInputFingerUped;
        _input.Disabled += OnInputFingerUped;
    }

    private void OnDisable()
    {
        _input.FingerDowned -= OnInputFingerDowned;
        _input.FingerMoved -= OnInputFingerMoved;
        _input.FingerUped -= OnInputFingerUped;
        _input.Disabled -= OnInputFingerUped;
    }

    private void OnInputFingerDowned(Vector2 position)
    {
        _joystick.position = position;
        _joystickPoint.anchoredPosition = Vector2.zero;
        _joystickOutCircle.gameObject.SetActive(true);
    }

    private void OnInputFingerMoved(Vector2 delta)
    {
        _joystickPoint.anchoredPosition = delta * HalfTouchScreenSize;
    }

    private void OnInputFingerUped()
    {
        _joystickOutCircle.gameObject.SetActive(false);
    }
}