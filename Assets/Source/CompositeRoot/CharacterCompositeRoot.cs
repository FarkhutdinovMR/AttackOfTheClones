using UnityEngine;

namespace CompositeRoot
{
    public class CharacterCompositeRoot : CompositeRoot
    {
        [SerializeField] private Config _config;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private HealthView _healthView;

        public override void Compose()
        {
            _health.Init(_config.PlayerHealth, _config.PlayerHealth);
        }

        private void OnEnable()
        {
            _health.Changed += _healthView.Render;
        }

        private void OnDisable()
        {
            _health.Changed -= _healthView.Render;
        }
    }
}