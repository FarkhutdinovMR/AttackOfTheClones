public interface ILevel
{
    public uint Value { get; }
    bool CanLevelUp { get; }
    void LevelUp();
}