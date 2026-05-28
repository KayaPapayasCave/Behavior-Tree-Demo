using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ClearAlert", story: "Guard didn't find Player and returns to patrol", category: "Action", id: "0cc72d4d30b9edbb1a2bdaea783f9a60")]
public partial class ClearAlertAction : Action
{

    [SerializeReference]
    public BlackboardVariable<bool> IsAlerted;

    protected override Status OnStart()
    {
        IsAlerted.Value = false;
        return Status.Success;
    }
}

