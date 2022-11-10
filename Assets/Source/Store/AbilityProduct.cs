using System;

public class AbilityProduct<T> : Product where T : Ability
{
    [NonSerialized] public T AbilityType;
}