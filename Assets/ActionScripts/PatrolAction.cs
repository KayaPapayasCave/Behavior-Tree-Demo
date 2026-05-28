using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable]
[GeneratePropertyBag]
[NodeDescription(
    name: "Patrol",
    story: "Guard patrols area",
    category: "Action",
    id: "patrol_action_v1"
)]
public partial class PatrolAction : Action
{
    [SerializeReference]
    public BlackboardVariable<GameObject> PatrolPoints;

    [SerializeReference]
    public BlackboardVariable<float> Speed;

    private readonly List<Transform> patrolPoints = new();
    private int index;

    protected override Status OnStart()
    {
        if (PatrolPoints == null || PatrolPoints.Value == null)
        {
            return Status.Failure;
        }

        patrolPoints.Clear();

        foreach (Transform child in PatrolPoints.Value.transform)
        {
            patrolPoints.Add(child);
        }

        index = 0;

        if (patrolPoints.Count == 0)
        {
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Transform selfT = GameObject.transform;

        Transform target = patrolPoints[index];

        selfT.position = Vector3.MoveTowards(
            selfT.position,
            target.position,
            Speed.Value * Time.deltaTime
        );

        if (Vector3.Distance(selfT.position, target.position) < 0.2f)
        {
            index = (index + 1) % patrolPoints.Count;
        }

        return Status.Running;
    }
}