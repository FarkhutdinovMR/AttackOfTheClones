using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class EnemiesDeathCounterView : MonoBehaviour, ICounterView
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