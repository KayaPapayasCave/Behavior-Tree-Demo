using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ClearAlert", story: "Return to patrol", category: "Action", id: "0cc72d4d30b9edbb1a2bdaea783f9a60")]
public partial class ClearAlertAction : Action
{
    // Reference til vagten
    [SerializeReference]
    public BlackboardVariable<GameObject> Guard;

    // Køres når Action noden starter
    protected override Status OnStart()
    {
        // Hent GuardAI komponenten fra vagten
        GuardAI guard = Guard.Value.GetComponent<GuardAI>();

        // Sæt isAlerted og canSeePlayer til false
        guard.isAlerted = false;
        guard.canSeePlayer = false;

        // Returner Success
        return Status.Success;
    }
}

