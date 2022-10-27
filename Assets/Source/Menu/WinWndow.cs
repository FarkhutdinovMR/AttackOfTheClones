using UnityEngine;
using UnityEngine.UI;

public class WinWndow : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private SceneLoader _sceneLoader;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(OnNextButtonClicked);
    }

    private void OnNextButtonClicked()
    {
        _sceneLoader.Restart();
    }
}