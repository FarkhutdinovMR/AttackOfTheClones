using UnityEngine;

namespace CompositeRoot
{
    public class CharacterCompositeRoot : CompositeRoot
    {
        [SerializeField] private Config _config;
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private TextPresenter _levelPresenter;
        [SerializeField] private AbilityUpgrade _abilityUpgrade;

        private Health _health;
        public CharacterLevel CharacterLevel { get; private set; }

        public override void Compose()
        {
            _health = new Health(_config.CharacterHealth);
            CharacterLevel = new CharacterLevel(0, 0, _config.CharacterLevelUp);
        }

        private void OnEnable()
        {
            _health.Changed += _healthView.Render;
            CharacterLevel.LevelChanged += OnCharacterLevelChanged;
        }

        private void OnDisable()
        {
            _health.Changed -= _healthView.Render;
            CharacterLevel.LevelChanged -= OnCharacterLevelChanged;
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