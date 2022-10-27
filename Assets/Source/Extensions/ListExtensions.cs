using System.Collections.Generic;

public static class ListExtensions
{
    public static State GetState<T>(this List<State> list) where T : State
    {
        return list.Find(state => state.GetType() == typeof(T));
    }
}