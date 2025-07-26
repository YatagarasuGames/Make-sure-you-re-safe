using System;

[AttributeUsage(AttributeTargets.Class)]
public class PlayerStateTypeAttribute : Attribute
{
    public PlayerStateType StateType { get; }

    public PlayerStateTypeAttribute(PlayerStateType stateType) => StateType = stateType;
}
