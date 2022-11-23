using System;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private const int LoaderScene = 1;
    private const int FirstSceneIndex = 1;

    public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;
    public int Amount => SceneManager.sceneCountInBuildSettings - LoaderScene;
    public int NextSceneIndex => CurrentSceneIndex < Amount ? CurrentSceneIndex + 1 : FirstSceneIndex;

    public void Restart()
    {
        Load(CurrentSceneIndex);
    }

    public void LoadNext()
    {
        Load(NextSceneIndex);
    }

    public void Load(int index)
    {
        if (index < 0 || index > Amount)
            throw new ArgumentOutOfRangeException(nameof(index));

        SceneManager.LoadScene(index);
    }
}