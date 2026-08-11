using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AlertNearbyGuards", story: "Alert nearby guards", category: "Action", id: "78ed7d7ff99fa04c6c0a798f1c0ca794")]
public partial class AlertNearbyGuardsAction : Action
{
    // Reference til Blackboard variabler
    [SerializeReference]
    public BlackboardVariable<Transform> GuardTransform;

    [SerializeReference]
    public BlackboardVariable<Transform> PlayerTransform;

    [SerializeReference]
    public BlackboardVariable<float> AlertRadius;

    [SerializeReference]
    public BlackboardVariable<Vector2> LastKnownPosition;

    protected override Status OnStart()
    {
        // Kontrollerer om vagten og player findes
        if (GuardTransform.Value == null)
        {
            return Status.Failure;
        }

        if (PlayerTransform.Value == null)
        {
            return Status.Failure;
        }

        // Find alle collider2D objekter inden for AlertRadius omkring vagten
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                GuardTransform.Value.position,
                AlertRadius.Value
            );

        // Loop igennem alle hits
        foreach (Collider2D hit in hits)
        {
            // Tjekker om hit er en anden guard med GuardAI komponenten
            GuardAI otherGuard =
                hit.GetComponent<GuardAI>();

            // Kontroller at objektet er en anden guard og ikke den samme som den nuværende guard
            if (otherGuard != null &&
                otherGuard != GuardTransform.Value.GetComponent<GuardAI>())
            {
                // Sæt den anden guard til at være alarmeret og opdater dens sidste kendte position
                otherGuard.isAlerted = true;
                otherGuard.lastKnownPosition =
                    LastKnownPosition.Value;
            }
        }

        // Alarmen er udført
        return Status.Success;
    }
}

