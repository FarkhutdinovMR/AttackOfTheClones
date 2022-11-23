using UnityEngine;

public class GetNearbyObject<TObject> : MonoBehaviour where TObject : Component
{
    [SerializeField] private float _radius;

    private Collider[] _colliders = new Collider[50];

    public TObject NearbyObject { get; private set; }

    private void Update()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders);
        NearbyObject = null;
        float nearbyObjectDistance = float.PositiveInfinity;
        for (int i = 0; i < count; i++)
        {
            Collider collider = _colliders[i];
            if (collider.gameObject == gameObject)
                continue;

            if (collider.TryGetComponent(out TObject detectedObject))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < nearbyObjectDistance)
                {
                    NearbyObject = detectedObject;
                    nearbyObjectDistance = distance;
                }
            }
        }
    }
}