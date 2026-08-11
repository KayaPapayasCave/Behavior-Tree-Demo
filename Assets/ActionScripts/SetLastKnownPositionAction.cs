using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetLastKnownPosition", story: "Save last known position", category: "Action", id: "d6897b3949b65c9592bb053b39056f7c")]
public partial class SetLastKnownPositionAction : Action
{
    // Referencer til blackboard vaariabler
    [SerializeReference]
    public BlackboardVariable<Transform> PlayerTransform;

    [SerializeReference]
    public BlackboardVariable<Vector2> LastKnownPosition;

    protected override Status OnStart()
    {
        // Hvis spilleren ikke findes, kan positonen ikke gemmes
        if (PlayerTransform.Value == null)
            return Status.Failure;

        // Gemmer spillerens nuværende position i LastKnownPosition
        LastKnownPosition.Value = PlayerTransform.Value.position;

        // Returnerer Success
        return Status.Success;
    }
}