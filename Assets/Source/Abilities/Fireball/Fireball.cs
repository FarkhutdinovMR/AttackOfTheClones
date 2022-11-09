using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _hitFX;

    private float _radius;
    private int _damage;

    public void Init(float raduis, int damage)
    {
        _radius = raduis;
        _damage = damage;
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
            return;

        var getObjectsInRadius = new GetObjectsInRadius<Bot>(_radius, transform);
        IEnumerable bots = getObjectsInRadius.Get();

        foreach (Bot bot in bots)
            bot.Health.TakeDamage(_damage);

        Instantiate(_hitFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}