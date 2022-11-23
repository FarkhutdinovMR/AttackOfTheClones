using UnityEngine;

public class GetNearbyBot : GetNearbyObject<Bot>, ITargetSource
{
    public Transform Target => NearbyObject != null ? NearbyObject.transform : null;
}