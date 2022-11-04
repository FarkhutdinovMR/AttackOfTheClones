using UnityEngine;

[RequireComponent (typeof(CharacterMovement))]
public class TornadoInfluence : MonoBehaviour
{
    [SerializeField] private float _suctionPower = 1f;
    [SerializeField] private float _rotatePower = 1f;
    private CharacterMovement _characterMovement;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void AddForce(Vector3 suctionForce, Vector3 rotateForce)
    {
        Vector3 force = (suctionForce * _suctionPower) + (rotateForce * _rotatePower);
        _characterMovement.AddForce(force);
    }
}