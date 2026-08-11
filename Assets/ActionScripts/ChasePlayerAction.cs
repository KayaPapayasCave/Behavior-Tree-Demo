using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChasePlayer", story: "Chase Player", category: "Action", id: "8471ee0a575e113db9aefb2c0bd6fbce")]
public partial class ChasePlayerAction : Action
{
    // Blackboard variabler som Actionen får fra Behavior Graphen. Variablerne bruges til at få adgang til de nødvendige data for at udføre handlingen.
    [SerializeReference]
    public BlackboardVariable<GameObject> Self;


    [SerializeReference]
    public BlackboardVariable<Transform> PlayerTransform;


    [SerializeReference]
    public BlackboardVariable<float> MoveSpeed;

    // Reference til GuardAI komponenten, som bruges til at få adgang til guardens egenskaber og metoder.
    private GuardAI guardAI;

    // OnStart metoden kaldes, når Actionen starter. Her hentes GuardAI komponenten fra Self GameObjectet.
    protected override Status OnStart()
    {
        guardAI = Self.Value.GetComponent<GuardAI>();

        return Status.Running;
    }

    // OnUpdate metoden kaldes hver frame, mens Actionen kører. Her udføres selve jagten på spilleren.
    // Hvis Self eller PlayerTransform er null, returneres Failure. Hvis guarden ikke kan se spilleren, returneres også Failure.
    // Hvis guarden kan se spilleren, flyttes Self GameObjectet mod spillerens position med en hastighed bestemt af MoveSpeed og der returneres Running.
    protected override Status OnUpdate()
    {
        if (Self.Value == null ||
            PlayerTransform.Value == null)
        {
            return Status.Failure;
        }

        if (!guardAI.canSeePlayer)
        {
            return Status.Failure;
        }

        Self.Value.transform.position =
            Vector2.MoveTowards(
                Self.Value.transform.position,
                PlayerTransform.Value.position,
                MoveSpeed.Value * Time.deltaTime
            );

        return Status.Running;
    }
}

