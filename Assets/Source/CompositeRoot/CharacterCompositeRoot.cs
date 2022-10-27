using UnityEngine;

namespace CompositeRoot
{
    public class CharacterCompositeRoot : CompositeRoot
    {
        [SerializeField] private Config _config;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private CharacterLevel _characterLevel;
        [SerializeField] private AbilityUpgrade _abilityUpgrade;
        [SerializeField] private TextPresenter _levelPresenter;

        public override void Compose()
        {
            _health.Init( _config.PlayerHealth);
        }

        private void OnEnable()
        {
            _health.Changed += _healthView.Render;
            _characterLevel.LevelChanged += OnCharacterLevelChanged;
        }

        private void OnDisable()
        {
            _health.Changed -= _healthView.Render;
            _characterLevel.LevelChanged -= OnCharacterLevelChanged;
        }

        private void OnCharacterLevelChanged(uint level)
        {
            Pause();
            _abilityUpgrade.OpenUpgradeWindow(Resume);
            _levelPresenter.Render(level);
        }

        private void Pause()
        {
            Time.timeScale = 0f;
        }

        private void Resume()
        {
            Time.timeScale = 1f;
        }
    }
}