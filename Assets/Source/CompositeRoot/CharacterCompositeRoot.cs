using UnityEngine;

namespace CompositeRoot
{
    public class CharacterCompositeRoot : CompositeRoot
    {
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private Transform _lookTarget;

        public override void Compose()
        {
            //_movement.SetTarget(_lookTarget);
        }
    }
}