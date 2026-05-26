using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChasePlayer", story: "Guard chases Player", category: "Action", id: "8471ee0a575e113db9aefb2c0bd6fbce")]
public partial class ChasePlayerAction : Action
{

    [SerializeReference]
    public BlackboardVariable<Transform> GuardTransform;

    [SerializeReference]
    public BlackboardVariable<Transform> PlayerTransform;

    [SerializeReference]
    public BlackboardVariable<float> MoveSpeed;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (PlayerTransform.Value == null)
            return Status.Failure;

        GuardTransform.Value.position =
            Vector2.MoveTowards(
                GuardTransform.Value.position,
                PlayerTransform.Value.position,
                MoveSpeed.Value * Time.deltaTime
            );

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

