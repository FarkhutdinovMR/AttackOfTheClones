using UnityEngine;

namespace CompositeRoot
{
    public class LevelCompositeRoot : CompositeRoot
    {
        [SerializeField] private ProgressBarView _levelProgressView;
        [SerializeField] private BotSpawner _botSpawner;

        private DeathCounter _deathCounter;
        private LevelProgress _levelProgress;

        public override void Compose()
        {
            _deathCounter = new DeathCounter();
            _levelProgress = new LevelProgress(_botSpawner.Amount);
            _botSpawner.Init(_deathCounter);
        }

        private void OnEnable()
        {
            _deathCounter.Changed += _levelProgress.OnBotDead;
            _levelProgress.Changed += _levelProgressView.Render;
        }

        private void OnDisable()
        {
            _deathCounter.Changed -= _levelProgress.OnBotDead;
            _levelProgress.Changed -= _levelProgressView.Render;
        }
    }
}