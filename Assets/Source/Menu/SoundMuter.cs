using UnityEngine;
using UnityEngine.UI;

public class SoundMuter : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private GameObject _soundOnIcon;
    [SerializeField] private GameObject _soundOffIcon;

    public void Start()
    {
        _toggle.isOn = !AudioListener.pause;
        ChangeIcon();
    }

    public void OnToggleValueChanged(bool value)
    {
        AudioListener.pause = !value;
        ChangeIcon();
    }

    private void ChangeIcon()
    {
        _soundOnIcon.SetActive(_toggle.isOn);
        _soundOffIcon.SetActive(!_soundOnIcon.activeSelf);
    }
}