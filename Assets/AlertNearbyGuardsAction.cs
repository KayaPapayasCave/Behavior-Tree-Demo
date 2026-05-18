using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AlertNearbyGuards", story: "Guard alerts other nearby Guards", category: "Action", id: "78ed7d7ff99fa04c6c0a798f1c0ca794")]
public partial class AlertNearbyGuardsAction : Action
{

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

