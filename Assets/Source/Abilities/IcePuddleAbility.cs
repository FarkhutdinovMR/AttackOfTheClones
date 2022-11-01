using System.Collections;
using UnityEngine;

public class IcePuddleAbility : Ability
{
    [SerializeField] private IcePuddle _icePuddle;
    [SerializeField] private float _growSpeed = 1f;
    [SerializeField] private float _waitBeforeDisappear = 1f;
    [SerializeField] private ParticleSystem[] _particleSystems;

    public override IEnumerator Use()
    {
        _icePuddle.Init(AttackDamage);
        _icePuddle.Reset();
        PlayFX();
        float puddleSize = 0f;

        while (puddleSize < AttackRadius)
        {
            puddleSize += _growSpeed * Time.deltaTime;
            _icePuddle.transform.localScale = new Vector3(puddleSize, puddleSize, puddleSize);
            yield return null;
        }

        StopFX();
        yield return new WaitForSeconds(_waitBeforeDisappear);
        _icePuddle.Disable();

        yield return null;
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