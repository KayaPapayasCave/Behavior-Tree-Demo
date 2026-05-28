using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToLastKnownPosition", story: "Guard moves to Players last known position", category: "Action", id: "bb3e3f5d3f2642be0f4d412785cfe559")]
public partial class MoveToLastKnownPositionAction : Action
{

    [SerializeReference]
    public BlackboardVariable<Transform> GuardTransform;

    [SerializeReference]
    public BlackboardVariable<Vector2> LastKnownPosition;

    [SerializeReference]
    public BlackboardVariable<float> MoveSpeed;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        GuardTransform.Value.position =
            Vector2.MoveTowards(
                GuardTransform.Value.position,
                LastKnownPosition.Value,
                MoveSpeed.Value * Time.deltaTime
            );

        float distance =
            Vector2.Distance(
                GuardTransform.Value.position,
                LastKnownPosition.Value
            );

        if (distance < 0.1f)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

