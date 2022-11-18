using UnityEngine;

public class Audio : MonoBehaviour
{
    private uint _muteCount;

    public bool Pause => _muteCount > 0;

    public void Init(uint muteCount)
    {
        _muteCount = muteCount;
        Setup();
    }

    public void AddMute()
    {
        _muteCount++;
        Setup();
    }

    public void RemoveMute()
    {
        _muteCount--;
        Setup();
    }

    private void Setup()
    {
        AudioListener.pause = Pause;
        AudioListener.volume = AudioListener.pause ? 0f : 1f;
    }
}