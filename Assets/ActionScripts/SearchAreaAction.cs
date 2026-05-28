using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SearchArea", story: "Guard searches area", category: "Action", id: "b2e35564a3535242b738e7158711ee83")]
public partial class SearchAreaAction : Action
{

    [SerializeReference]
    public BlackboardVariable<float> SearchDuration;

    private float timer;

    protected override Status OnStart()
    {
        timer = SearchDuration.Value;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

