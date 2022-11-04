using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _rotateAngle = 1f;
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
            Rotate2(target);
    }

    private void Rotate(TornadoInfluence target)
    {
        Vector3 direction = target.transform.position - transform.position;
        float _angle = Angle(transform.forward, direction, transform.right);
        _angle += _rotateAngle;
        var pointOnCircle = new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
        pointOnCircle *= direction.magnitude;
        pointOnCircle = transform.TransformPoint(pointOnCircle);
        Vector3 force = pointOnCircle - target.transform.position;
        //target.Rotate(force);
    }

    private void Rotate2(TornadoInfluence target)
    {
        Vector3 suctionForce = transform.position - target.transform.position;
        Vector3 rotateForce = Vector3.Cross(suctionForce, target.transform.up).normalized;
        target.AddForce(suctionForce, rotateForce);
    }

    private float Angle(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) > 90f) ? 360f - angle : angle;
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