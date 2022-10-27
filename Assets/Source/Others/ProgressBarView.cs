using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class ProgressBarView : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void Render(float value)
    {
        _slider.value = value;
    }
}