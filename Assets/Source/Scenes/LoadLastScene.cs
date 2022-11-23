using UnityEngine;

public class LoadLastScene : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private Character _character;

    private void Start()
    {
        var saver = new PlayerPrefsJSONSaver(_config, _character);
        var sceneLoader = new SceneLoader();
        sceneLoader.Load(saver.Load().NextLevel);
    }
}