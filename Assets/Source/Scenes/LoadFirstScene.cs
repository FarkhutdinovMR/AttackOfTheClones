using UnityEngine;

public class LoadFirstScene : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader.LoadFirtsScene();
    }
}