using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private float _waitBeforeDestroy = 0.2f;

    private Transform _destination;
    private int _damage;

    public void Init(int damage, Transform destination)
    {
        _damage = damage;
        _destination = destination;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        if (_destination != null)
            transform.position = Vector3.MoveTowards(transform.position, _destination.position, _speed * Time.deltaTime);
        else
            transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot))
        {
            bot.Health.TakeDamage(_damage);
            Destroy(gameObject, _waitBeforeDestroy);
        }
    }
}