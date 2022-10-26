using UnityEngine;

public class CharacterLevel : MonoBehaviour
{
    private uint _currentLevel;
    private uint _exp;

    public void Init(uint currentLevel, uint exp)
    {
        _currentLevel = currentLevel;
        _exp = exp;
    }

    public void AddExp(uint value)
    {
        _exp += value;
    }
}