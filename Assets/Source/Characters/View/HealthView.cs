using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class HealthView : MonoBehaviour, IHealthView
{
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private Slider _slider;

    public void Render(float value)
    {
        _slider.value = value;
    }
}