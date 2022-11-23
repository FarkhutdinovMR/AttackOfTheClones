using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator _startMenuAnimator;
    [SerializeField] private MonoBehaviour _storeUISource;
    private IStoreUI _storeUI => (IStoreUI)_storeUISource;

    [SerializeField] private PauseMenu _pauseMenu;

    private const string HideMenuAnimation = "HideMenuAnimation";

    private void OnValidate()
    {
        if (_storeUISource && !(_storeUISource is IStoreUI))
        {
            Debug.LogError(_storeUISource + " not implement " + nameof(IStoreUI));
            _storeUISource = null;
        }
    }

    public void HideStartingMenu()
    {
        _startMenuAnimator.Play(HideMenuAnimation);
    }

    public void OnStoreButtonClick()
    {
        _storeUI.Open();
    }

    public void OnPauseButtonClick()
    {
        _pauseMenu.Pause();
    }

    public void OnResumeButtonClick()
    {
        _pauseMenu.Resume();
    }
}