using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckPlayerDetection", story: "Player detection", category: "Action", id: "3b995dec8b757bf0a917647e3288db8b")]
public partial class CheckPlayerDetectionAction : Action
{
    // Reference til vagtens transform
    [SerializeReference]
    public BlackboardVariable<Transform> GuardTransform;

    // Reference til spillerens transform
    [SerializeReference]
    public BlackboardVariable<Transform> PlayerTransform;

    // Reference til detektions range
    [SerializeReference]
    public BlackboardVariable<float> DetectionRange;

    // Kører løbende, mens detectionen er aktiv
    protected override Status OnUpdate()
    {
        // Hent GuardAI komponenten fra vagten
        GuardAI guard = GuardTransform.Value.GetComponent<GuardAI>();

        // Hvis GuardAI komponenten ikke findes på vagten, returner Failure
        if (guard == null)
            return Status.Failure;

        // Beregn afstanden mellem vagten og spilleren
        float distance =
            Vector2.Distance(
                GuardTransform.Value.position,
                PlayerTransform.Value.position
            );

        // Hvis afstanden er større end detektions range, kan vagten ikke se spilleren
        if (distance > DetectionRange.Value)
        {
            guard.canSeePlayer = false;
            return Status.Running;
        }

        // Beregn retningen fra vagten til spilleren
        Vector2 direction =
            (PlayerTransform.Value.position -
             GuardTransform.Value.position).normalized;

        // Udfør en raycast fra vagten mod spilleren for at tjekke om der er nogen vægge imellem
        RaycastHit2D hit =
            Physics2D.Raycast(
                GuardTransform.Value.position,
                direction,
                distance,
                LayerMask.GetMask("Walls")
            );

        // Hvis raycasten rammer en væg, kan vagten ikke se spilleren
        if (hit.collider != null)
        {
            guard.canSeePlayer = false;
            return Status.Running;
        }

        // Hvis vagten kan se spilleren, sæt canSeePlayer til true og opdater lastKnownPosition
        guard.canSeePlayer = true;

        guard.lastKnownPosition =
            PlayerTransform.Value.position;

        return Status.Running;
    }
}

