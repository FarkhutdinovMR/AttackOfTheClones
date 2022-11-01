using System.Collections;
using UnityEngine;

public class IcePuddle : MonoBehaviour
{
    private int _damage;
    private float _runningTime;
    private float _damageFrequency = 0.25f;

    private float _radius => transform.localScale.x * 0.5f;

    public void Init(int damage)
    {
        _damage = damage;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);

        _runningTime += Time.deltaTime;
        if (_runningTime >= _damageFrequency)
        {
            Damage();
            _runningTime = 0f;
        }
    }

    public void Reset()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void Damage()
    {
        var getObjectsInRadius = new GetObjectsInRadius<Bot>(_radius, transform);
        IEnumerable bots = getObjectsInRadius.Get();

        foreach (Bot bot in bots)
            bot.Health.TakeDamage(_damage);
    }
}