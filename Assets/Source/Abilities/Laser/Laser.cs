using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;
    [SerializeField] private float _delayBeforeDestroy;
    [SerializeField] private ParticleSystem[] _fx;

    private int _damage;

    public void Init(int damage)
    {
        _damage = damage;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;

            if (Physics.SphereCast(transform.position, _radius, transform.forward, out RaycastHit hitInfo, _radius))
            {
                if (hitInfo.transform.TryGetComponent(out Bot bot))
                    bot.Health.TakeDamage(_damage);

                foreach (ParticleSystem particle in _fx)
                    particle.Stop();

                Destroy(gameObject, _delayBeforeDestroy);
                break;
            }

            yield return null;
        }
    }
}