using UnityEngine;

public class LoadFirstScene : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Saver _save;

    private void Start()
    {
        Saver.Data data = _save.Load();
        _sceneLoader.Load(data.NextLevel);
    }
}