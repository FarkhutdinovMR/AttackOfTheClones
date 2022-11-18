using Agava.WebUtility;
using UnityEngine;

public class MuteInBackground : MonoBehaviour
{
    [SerializeField] private Audio _audio;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (inBackground)
            _audio.AddMute();
        else
            _audio.RemoveMute();
    }
}