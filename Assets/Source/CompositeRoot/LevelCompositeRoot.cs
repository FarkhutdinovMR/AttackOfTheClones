using System.Collections;
using UnityEngine;

public class LevelCompositeRoot : MonoBehaviour
{
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private ProgressBarView _levelProgressView;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _failWindow;
    [SerializeField] private Character _character;
    [SerializeField] private float _waitTimeBeforeEndGame;

    private Counter _deathCounter;

    private void Awake()
    {
        _deathCounter = new Counter(_botSpawner.Amount);
        _botSpawner.Init(_deathCounter);
    }

    private void OnEnable()
    {
        _deathCounter.Changed += OnDeathCounterChanged;
        _deathCounter.Complited += OnDeathCounterComplited;
        _character.Health.Died += OnCharacterDied;
    }

    private void OnDisable()
    {
        _deathCounter.Changed -= OnDeathCounterChanged;
        _deathCounter.Complited -= OnDeathCounterComplited;
        _character.Health.Died -= OnCharacterDied;
    }

    private void OnDeathCounterChanged(int count)
    {
        _levelProgressView.Render((float)count / _botSpawner.Amount);
    }

    private void OnDeathCounterComplited()
    {
        CompleteLevel();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
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
        _winWindow.SetActive(true);
    }

    private IEnumerator OpenFailWindow()
    {
        yield return new WaitForSeconds(_waitTimeBeforeEndGame);
        Pause();
        _failWindow.SetActive(true);
    }
}