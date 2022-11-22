using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Root _root;
    [SerializeField] private Audio _audio;

    public void Pause()
    {
        gameObject.SetActive(true);
        _root.Pause();
        _audio.AddMute();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        _root.Resume();
        _audio.RemoveMute();
    }
}