using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TornadoSpawner : MonoBehaviour
{
    [SerializeField] private Tornado _tornadoTemplate;
    [SerializeField] private float _raduis = 10f;
    [SerializeField] private int _amount = 5;
    [SerializeField] private float _waitAfterWarning = 6f;
    [SerializeField] private int _minSpawnTime = 10;
    [SerializeField] private int _maxSpawnTime = 20;
    [SerializeField] private Transform _character;
    [SerializeField] private UnityEvent _tornadoAppearance;

    public event UnityAction TornadoAppearance
    {
        add => _tornadoAppearance.AddListener(value);
        remove => _tornadoAppearance.RemoveListener(value);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

        _tornadoAppearance?.Invoke();
        yield return new WaitForSeconds(_waitAfterWarning);

        for (int i = 0; i < _amount; i++)
        {
            Vector3 position = Random.insideUnitCircle * _raduis;
            position = _character.TransformPoint(new Vector3(position.x, 0f, position.y));
            Instantiate(_tornadoTemplate, position, Quaternion.identity, transform);

            yield return new WaitForSeconds(2f);
        }
    }
}