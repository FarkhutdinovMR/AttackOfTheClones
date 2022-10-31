using System.Collections;
using UnityEngine;

public class LevelCompositeRoot : CompositeRoot
{
    [SerializeField] private WaveSpawner _spawner;
    [SerializeField] private ProgressBarView _levelProgressView;
    [SerializeField] private TextView _currentLevelView;
    [SerializeField] private WinWndow _winWindow;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private Character _character;
    [SerializeField] private float _waitTimeBeforeEndGame;
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private SceneLoader _sceneLoader;

    public Counter DeathCounter { get; private set; }

    public override void Compose()
    {
        DeathCounter = new Counter(_spawner.Amount);
    }

    private void OnEnable()
    {
        DeathCounter.Changed += OnDeathCounterChanged;
        DeathCounter.Complited += OnDeathCounterComplited;
        _character.Health.Died += OnCharacterDied;
    }

    private void OnDisable()
    {
        DeathCounter.Changed -= OnDeathCounterChanged;
        DeathCounter.Complited -= OnDeathCounterComplited;
        _character.Health.Died -= OnCharacterDied;
    }

    private void Start()
    {
        _currentLevelView.Render(_sceneLoader.CurrentSceneIndex);
    }

    private void OnDeathCounterChanged(int count)
    {
        _levelProgressView.Render((float)count / _spawner.Amount);
    }

    private void OnDeathCounterComplited()
    {
        CompleteLevel();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _character.Input.Disable();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _character.Input.Enable();
    }

    private void CompleteLevel()
    {
        StartCoroutine(OpenWinWindow());
    }

    private void OnCharacterDied()
    {
        StartCoroutine(OpenFailWindow());
    }

    private IEnumerator OpenWinWindow()
    {
        yield return new WaitForSeconds(_waitTimeBeforeEndGame);
        Pause();
        _winWindow.Open(Resume);
    }

    private IEnumerator OpenFailWindow()
    {
        yield return new WaitForSeconds(_waitTimeBeforeEndGame);
        Pause();
        _failWindow.Open(Resume);
    }
}