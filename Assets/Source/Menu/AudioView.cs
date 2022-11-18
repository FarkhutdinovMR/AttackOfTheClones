using UnityEngine;
using UnityEngine.UI;

public class AudioView : MonoBehaviour
{
    [SerializeField] private Button _audioOnButton;
    [SerializeField] private Button _audioOffButton;
    [SerializeField] private Audio _audio;

    public void Start()
    {
        Render();
    }

    public void OnAudioOnButton()
    {
        _audio.AddMute();
        Render();
    }

    public void OnAudioOffButton()
    {
        _audio.RemoveMute();
        Render();
    }

    private void Render()
    {
        _audioOnButton.gameObject.SetActive(!_audio.Pause);
        _audioOffButton.gameObject.SetActive(!_audioOnButton.isActiveAndEnabled);
    }
}