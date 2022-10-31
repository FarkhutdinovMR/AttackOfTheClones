using System.Collections;
using UnityEngine;

public class IcePuddleAbility : Ability
{
    [SerializeField] private IcePuddle _icePuddle;
    [SerializeField] private float _growSpeed = 1f;
    [SerializeField] private float _waitBeforeDisappear = 1f;

    public override void Use()
    {
        _icePuddle.Init(AttackDamage);
        _icePuddle.transform.localScale = Vector3.zero;
        _icePuddle.gameObject.SetActive(true);
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        float size = 0f;
        while (size < AttackRadius)
        {
            size += _growSpeed * Time.deltaTime;
            _icePuddle.transform.localScale = new Vector3(size, size, size);
            yield return null;
        }

        yield return new WaitForSeconds(_waitBeforeDisappear);
        _icePuddle.gameObject.SetActive(false);
    }
}