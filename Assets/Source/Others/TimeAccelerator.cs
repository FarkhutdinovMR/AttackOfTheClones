using System.Collections;
using UnityEngine;

public class TimeAccelerator : MonoBehaviour
{
    [SerializeField] private float _timeScale;
    [SerializeField] private float _period;

    private const float DefaultTimeScale = 1f;

    public void SpeedUp()
    {
        Time.timeScale = _timeScale;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(_period);
        RestoreDefault();
    }

    private void RestoreDefault()
    {
        Time.timeScale = DefaultTimeScale;
    }
}