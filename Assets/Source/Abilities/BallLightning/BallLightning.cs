using System.Collections;
using UnityEngine;

public class BallLightning : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _attackFrequence;
    [SerializeField] private Lightning _lightningTemplate;
    [SerializeField] private float _lifeTime = 6f;

    private float _radius;
    private int _damage;

    public void Init(float raduis, int damage)
    {
        _radius = raduis;
        _damage = damage;
        InvokeRepeating(nameof(Attack), 0f, _attackFrequence);
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void Attack()
    {
        var getObjectsInRadius = new GetObjectsInRadius<Bot>(_radius, transform);
        IEnumerable bots = getObjectsInRadius.Get();

        foreach (Bot bot in bots)
        {
            Lightning newLightning = Instantiate(_lightningTemplate, transform);
            newLightning.Init(_damage, bot.transform);
        }
    }
}