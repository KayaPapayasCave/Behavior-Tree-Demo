using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AlertNearbyGuards", story: "Guard alerts other nearby Guards", category: "Action", id: "78ed7d7ff99fa04c6c0a798f1c0ca794")]
public partial class AlertNearbyGuardsAction : Action
{

    [SerializeReference]
    public BlackboardVariable<Transform> GuardTransform;

    [SerializeReference]
    public BlackboardVariable<Transform> PlayerTransform;

    [SerializeReference]
    public BlackboardVariable<float> AlertRadius;

    protected override Status OnStart()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                GuardTransform.Value.position,
                AlertRadius.Value
            );

        foreach (Collider2D hit in hits)
        {
            GuardAI otherGuard =
                hit.GetComponent<GuardAI>();

            if (otherGuard != null)
            {
                otherGuard.isAlerted = true;
                otherGuard.lastKnownPosition =
                    PlayerTransform.Value.position;
            }
        }

        return Status.Success;
    }
}

