using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int _currentSceneIndex;

    private const int loaderScene = 1;
    private const int _firstSceneIndex = 1;

    public int Amount => SceneManager.sceneCountInBuildSettings - loaderScene;
    public int NextScene => _currentSceneIndex < Amount ? _currentSceneIndex + 1 : _firstSceneIndex;
    public int CurrentSceneIndex => _currentSceneIndex;

    public void Init()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void Restart()
    {
        Load(_currentSceneIndex);
    }

    public void LoadNext()
    {
        _currentSceneIndex = NextScene;
        Load(_currentSceneIndex);
    }

    public void LoadFirtsScene()
    {
        Load(_firstSceneIndex);
    }

    public void Load(int index)
    {
        if (index > Amount)
            throw new ArgumentOutOfRangeException(nameof(index));

        SceneManager.LoadScene(index);
    }
}