using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [field: SerializeField] public List<State> States { get; private set; }

    private List<State> _baseStates;
    protected GetNearbyBot NearbyBot { get; private set; }

    public float AttackRadius => States.GetState<AttackRadiusState>().Value + _baseStates.GetState<AttackRadiusState>().Value;
    public int AttackDamage => (int)(States.GetState<AttackDamageState>().Value + _baseStates.GetState<AttackDamageState>().Value);
    public float AttackInterval => States.GetState<AttackIntervalState>().Value + _baseStates.GetState<AttackIntervalState>().Value;
    public Transform Target => NearbyBot.NearbyObject.transform;

    public void Init(List<State> baseStates, GetNearbyBot getNearby)
    {
        _baseStates = baseStates;
        NearbyBot = getNearby;
        StartCoroutine(Play());
    }

    public IEnumerator Play()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackInterval);

            if (NearbyBot.NearbyObject == null)
                continue;

            yield return Use();
        }
    }

    public abstract IEnumerator Use();
}