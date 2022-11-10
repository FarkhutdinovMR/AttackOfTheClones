using System.Collections;
using UnityEngine;

public class IcePuddleWeapon : Weapon
{
    [SerializeField] private IcePuddle _icePuddle;
    [SerializeField] private float _growSpeed = 1f;
    [SerializeField] private float _waitBeforeDisappear = 1f;
    [SerializeField] private ParticleSystem[] _particleSystems;

    public override void Use()
    {
        StartCoroutine(Repeat());
    }

    public IEnumerator Repeat()
    {
        while (true)
        {
            _icePuddle.Init((int)Slot.GetValue(typeof(AttackDamage)));
            _icePuddle.Reset();
            PlayFX();
            float puddleSize = 0f;

            while (puddleSize < Slot.GetValue(typeof(AttackRadius)))
            {
                puddleSize += _growSpeed * Time.deltaTime;
                _icePuddle.transform.localScale = new Vector3(puddleSize, puddleSize, puddleSize);
                yield return null;
            }

            StopFX();
            yield return new WaitForSeconds(_waitBeforeDisappear);
            _icePuddle.Disable();

            yield return new WaitForSeconds(Slot.GetValue(typeof(AttackRadius)));
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