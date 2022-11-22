using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraDistance : MonoBehaviour
{
    [SerializeField] private AnimationCurve _distanceCurve;
    [SerializeField] private AnimationCurve _shoulderOffsetCurve;

    private CinemachineVirtualCamera _camera;
    private Cinemachine3rdPersonFollow _cinemachine3RdPersonFollow;
    private float _aspectRatio;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        CinemachineComponentBase componentBase = _camera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        _cinemachine3RdPersonFollow = componentBase as Cinemachine3rdPersonFollow;
    }

    private void Update()
    {
        float newAspectRatio = (float)Screen.width / Screen.height;

        if (Mathf.Approximately(newAspectRatio, _aspectRatio) == false)
        {
            _aspectRatio = newAspectRatio;
            float distance = _distanceCurve.Evaluate(_aspectRatio);
            float shoulderOffset = _shoulderOffsetCurve.Evaluate(_aspectRatio);
            _cinemachine3RdPersonFollow.CameraDistance = distance;
            _cinemachine3RdPersonFollow.ShoulderOffset.y = shoulderOffset;
        }
    }
}