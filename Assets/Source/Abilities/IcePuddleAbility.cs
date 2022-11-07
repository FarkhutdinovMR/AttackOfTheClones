using System.Collections;
using UnityEngine;

public class IcePuddleAbility : Ability, IAbility
{
    [SerializeField] private IcePuddle _icePuddle;
    [SerializeField] private float _growSpeed = 1f;
    [SerializeField] private float _waitBeforeDisappear = 1f;
    [SerializeField] private ParticleSystem[] _particleSystems;

    protected override void Init(Saver.Data saver)
    {
        States.Add(new IcePuddleAttackDamage(StateConfigs.Find(config => config.Type == StateType.AttackDamage), saver.GetStateLevel(typeof(IcePuddleAttackDamage))));
        States.Add(new IcePuddleAttackInterval(StateConfigs.Find(config => config.Type == StateType.AttackInterval), saver.GetStateLevel(typeof(IcePuddleAttackInterval))));
        States.Add(new IcePuddleAttackRadius(StateConfigs.Find(config => config.Type == StateType.AttackRadius), saver.GetStateLevel(typeof(IcePuddleAttackRadius))));
    }

    public void Use()
    {
        StartCoroutine(Repeat());
    }

    public IEnumerator Repeat()
    {
        while (true)
        {
            _icePuddle.Init((int)Slot.GetValue(StateType.AttackDamage));
            _icePuddle.Reset();
            PlayFX();
            float puddleSize = 0f;

            while (puddleSize < Slot.GetValue(StateType.AttackRadius))
            {
                puddleSize += _growSpeed * Time.deltaTime;
                _icePuddle.transform.localScale = new Vector3(puddleSize, puddleSize, puddleSize);
                yield return null;
            }

            StopFX();
            yield return new WaitForSeconds(_waitBeforeDisappear);
            _icePuddle.Disable();

            yield return new WaitForSeconds(Slot.GetValue(StateType.AttackInterval));
        }
    }

    private void StopFX()
    {
        foreach (ParticleSystem particleSystem in _particleSystems)
            particleSystem.Stop();
    }

    private void PlayFX()
    {
        foreach (ParticleSystem particleSystem in _particleSystems)
            particleSystem.Play();
    }
}