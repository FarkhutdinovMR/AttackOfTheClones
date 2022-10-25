using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class HealthView : MonoBehaviour
{
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private Slider _slider;

    public void Render(int value, int MaxValue)
    {
        _slider.value = (float)value / MaxValue;
    }
}