using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable]
[GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "Patrol", category: "Action", id: "patrol_action_v1")]
public partial class PatrolAction : Action
{
    // Reference til Blackboard variabler
    [SerializeReference]
    public BlackboardVariable<GameObject> PatrolPoints;

    [SerializeReference]
    public BlackboardVariable<float> Speed;

    // Liste til at holde de faktiske Transform komponenter af patruljepunkterne
    private readonly List<Transform> patrolPoints = new();

    // Reference til GuardAI komponenten
    private GuardAI guardAI;

    protected override Status OnStart()
    {
        // Hent GuardAI komponenten fra GameObject
        guardAI = GameObject.GetComponent<GuardAI>();

        // Hvis patruljepunkterne ikke er sat, returner Failure
        if (PatrolPoints == null || PatrolPoints.Value == null)
            return Status.Failure;

        // Ryd listen over patruljepunkter og fyld den med de aktuelle punkter (sikkerhed)
        patrolPoints.Clear();

        // Tilføj alle child af PatrolPoints GameObject til listen over patruljepunkter
        foreach (Transform child in PatrolPoints.Value.transform)
        {
            patrolPoints.Add(child);
        }

        // Hvis der ikke er nogen patruljepunkter, returner Failure
        if (patrolPoints.Count == 0)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Stopper patrol hvis vagten er alarmeret eller kan se spilleren
        if (guardAI.isAlerted || guardAI.canSeePlayer)
        {
            return Status.Failure;
        }

        // Find det nuværende patruljepunkt baseret på guardAI's patrolIndex
        Transform target = patrolPoints[guardAI.patrolIndex];

        // Flyt vagt GameObject mod det nuværende patruljepunkt
        GameObject.transform.position =
            Vector3.MoveTowards(
                GameObject.transform.position,
                target.position,
                Speed.Value * Time.deltaTime
            );

        // Hvis vagt GameObject er tæt nok på patruljepunktet, opdater patrolIndex til næste punkt
        if (Vector3.Distance(GameObject.transform.position, target.position) < 0.2f)
        {
            guardAI.patrolIndex =
                (guardAI.patrolIndex + 1) % patrolPoints.Count;
        }

        // Fortsæt med at køre patruljen
        return Status.Running;
    }
}