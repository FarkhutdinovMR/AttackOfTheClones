using System;

[Serializable]
public class CharacterScore : Score
{
    private readonly IExperience _experience;

    public CharacterScore(uint value, IExperience experience) : base(value)
    {
        _experience = experience ?? throw new NullReferenceException(nameof(experience));
    }

    public override void Add(uint value)
    {
        base.Add(value);
        _experience.Add(value);
    }
}