using System;

public class LevelProgress
{
    private readonly int _total;

    private float _value;

    public event Action<float> Changed;

    public LevelProgress(int total)
    {
        _total = total;
    }

    public void OnBotDead(int count)
    {
        _value = (float)count / _total;
        Changed?.Invoke(_value);
    }
}