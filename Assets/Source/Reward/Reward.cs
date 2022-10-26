using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _target;
    private uint _reward;

    public void Init(uint reward, Transform target)
    {
        _reward = reward;
        _target = target;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterLevel character))
        {
            character.AddExp(_reward);
            Destroy(gameObject);
        }
    }
}