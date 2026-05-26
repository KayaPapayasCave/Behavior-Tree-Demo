using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetLastKnownPosition", story: "Guard saves the last known position of the Player", category: "Action", id: "d6897b3949b65c9592bb053b39056f7c")]
public partial class SetLastKnownPositionAction : Action
{
    [SerializeReference]
    public BlackboardVariable<Transform> PlayerTransform;

    [SerializeReference]
    public BlackboardVariable<Vector2> LastKnownPosition;

    protected override Status OnStart()
    {
        if (PlayerTransform.Value == null)
            return Status.Failure;

        LastKnownPosition.Value = PlayerTransform.Value.position;

        return Status.Success;
    }
}

