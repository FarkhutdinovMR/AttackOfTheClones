using System;
using UnityEngine;

public class BotSpawner : ISpawner
{
    private readonly Bot _template;
    private readonly Character _character;
    private readonly Transform _container;    
    private readonly Counter _deathCounter;

    public BotSpawner(Bot template, Character character, Transform container, Counter deathCounter)
    {
        _template = template ?? throw new ArgumentNullException(nameof(template));
        _character = character ?? throw new ArgumentNullException(nameof(character));
        _container = container ?? throw new ArgumentNullException(nameof(container));
        _deathCounter = deathCounter ?? throw new ArgumentNullException(nameof(deathCounter));
    }

    public void Spawn(Vector3 position)
    {
        Bot newBot = MonoBehaviour.Instantiate(_template, position, Quaternion.identity, _container);
        newBot.Init(_character, _deathCounter);
    }
}