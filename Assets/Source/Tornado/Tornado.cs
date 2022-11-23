using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _liveTime = 20f;
    [SerializeField] private float _moveRadius = 20f;
    [SerializeField] private float _waitTimeBeforeDisable = 3f;
    [SerializeField] private ParticleSystem[] _particleSystems;

    private void Start()
    {
        StartCoroutine(Life—ycle());
        StartCoroutine(Move());
    }

    private void Update()
    {
        RotateObjects();
    }

    private void RotateObjects()
    {
        var result = new GetObjectsInRadius<TornadoInfluence>(_radius, transform);
        IEnumerable<TornadoInfluence> _targets = result.Get();
        foreach (TornadoInfluence target in _targets)
            Rotate(target);
    }

    private void Rotate(TornadoInfluence target)
    {
        Vector3 suctionForce = transform.position - target.transform.position;
        Vector3 rotateForce = Vector3.Cross(suctionForce, target.transform.up).normalized;
        target.AddForce(suctionForce, rotateForce);
    }

    private IEnumerator Life—ycle()
    {
        yield return new WaitForSeconds(_liveTime);

        foreach (ParticleSystem particle in _particleSystems)
            particle.Stop();

        yield return new WaitForSeconds(_waitTimeBeforeDisable);
        Destroy(gameObject);
    }

    private IEnumerator Move()
    {
        while(true)
        {
            Vector3 destination = Random.insideUnitCircle * _moveRadius;
            destination = transform.TransformPoint(new Vector3(destination.x, 0f, destination.y));
            Vector3 startPosition = transform.position;
            float evalution = 0f;

            while(evalution < 1f)
            {
                evalution += _moveSpeed * Time.deltaTime;
                transform.position = Vector3.Slerp(startPosition, destination, evalution);
                yield return null;
            }
        }
    }
}