using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "Guard patrols area", category: "Action", id: "ed83db7908ae3a4782691bbdcd26894f")]
public partial class PatrolAction : Action
{

    [SerializeReference]
    public BlackboardVariable<Transform> GuardTransform;

    [SerializeReference]
    public BlackboardVariable<Transform[]> PatrolPoints;

    [SerializeReference]
    public BlackboardVariable<float> MoveSpeed;

    private int currentIndex;

    protected override Status OnStart()
    {
        currentIndex = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (PatrolPoints.Value.Length == 0)
            return Status.Failure;

        Transform target = PatrolPoints.Value[currentIndex];

        GuardTransform.Value.position =
            Vector2.MoveTowards(
                GuardTransform.Value.position,
                target.position,
                MoveSpeed.Value * Time.deltaTime
            );

        float distance =
            Vector2.Distance(
                GuardTransform.Value.position,
                target.position
            );

        if (distance < 0.1f)
        {
            currentIndex++;

            if (currentIndex >= PatrolPoints.Value.Length)
            {
                currentIndex = 0;
            }
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

