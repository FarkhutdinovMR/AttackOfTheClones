using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SetMovementInput : Action
{
    public Vector2 Direction;
    public SharedBotInput SelfBotInput;

    public override TaskStatus OnUpdate()
    {
        SelfBotInput.Value.MovementInput = Direction;
        return TaskStatus.Success;
    }
}