using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Awake()
    {
        float randomAngle = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(transform.rotation.x, randomAngle, transform.rotation.z);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _speed * Time.deltaTime);
    }
}