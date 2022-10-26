using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private float _interval;

    [field: SerializeField] protected int Damage { get; private set; }
    [field:SerializeField] protected float Radius { get; private set; }

    private GetNearbyBot _nearbyBot;

    protected Transform StartPoint { get; private set; }

    public void Init(GetNearbyBot getNearby, Transform startPoint)
    {
        _nearbyBot = getNearby;
        StartPoint = startPoint;
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        while(true)
        {
            yield return new WaitForSeconds(_interval);

            if (_nearbyBot.NearbyObject == null)
                continue;

            Attack(_nearbyBot.NearbyObject.transform);
        }
    }

    protected abstract void Attack(Transform target);
}