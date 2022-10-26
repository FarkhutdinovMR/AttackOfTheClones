using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float _time;

    private void Start()
    {
        Destroy(gameObject, _time);
    }
}