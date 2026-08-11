using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToLastKnownPosition", story: "Move", category: "Action", id: "bb3e3f5d3f2642be0f4d412785cfe559")]
public partial class MoveToLastKnownPositionAction : Action
{
    // Referencer til blackboard variabler
    [SerializeReference]
    public BlackboardVariable<Transform> GuardTransform;

    [SerializeReference]
    public BlackboardVariable<Vector2> LastKnownPosition;

    [SerializeReference]
    public BlackboardVariable<float> MoveSpeed;

    protected override Status OnUpdate()
    {
        // Hvis vagten ikke findes, returner failure
        if (GuardTransform.Value == null)
            return Status.Failure;

        // Hent GuardAI komponenten fra vagten
        GuardAI guard =
            GuardTransform.Value.GetComponent<GuardAI>();

        // Hvis GuardAI komponenten ikke findes, returner failure
        if (guard == null)
            return Status.Failure;

        // Hent den sidste kendte position fra GuardAI komponenten
        Vector2 targetPosition = guard.lastKnownPosition;

        // Flyt vagten mod den sidste kendte position
        GuardTransform.Value.position =
            Vector2.MoveTowards(
                GuardTransform.Value.position,
                targetPosition,
                MoveSpeed.Value * Time.deltaTime
            );

        // Beregn afstanden mellem vagten og den sidste kendte position
        float distance =
            Vector2.Distance(
                GuardTransform.Value.position,
                targetPosition
            );

        // Hvis afstanden er mindre end 0.1, returner success (tæt nok på)
        if (distance < 0.1f)
        {
            return Status.Success;
        }

        // Fortsæt med at køre, hvis vagten ikke er tæt nok på den sidste kendte position
        return Status.Running;
    }
}

