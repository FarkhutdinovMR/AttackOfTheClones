using System;
using UnityEngine.SceneManagement;

public class SceneLoader : CompositeRoot
{
    private int _currentSceneIndex;

    private const int loaderScene = 1;
    private const int _firstSceneIndex = 1;

    public int Amount => SceneManager.sceneCountInBuildSettings - loaderScene;
    public int NextStage => _currentSceneIndex < Amount ? _currentSceneIndex + 1 : _firstSceneIndex;
    public int CurrentSceneIndex => _currentSceneIndex;

    public override void Compose()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void Restart()
    {
        Load(_currentSceneIndex);
    }

    public void LoadNext()
    {
        _currentSceneIndex = NextStage;
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