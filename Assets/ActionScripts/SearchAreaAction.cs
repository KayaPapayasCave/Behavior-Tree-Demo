using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SearchArea", story: "Search", category: "Action", id: "b2e35564a3535242b738e7158711ee83")]
public partial class SearchAreaAction : Action
{
    // Reference til blackboard variabel for søgedurationen
    [SerializeReference]
    public BlackboardVariable<float> SearchDuration;

    // Timer til at holde styr på den resterende søgetid
    private float timer;

    protected override Status OnStart()
    {
        // Initialiser timeren med værdien fra blackboard variablen
        timer = SearchDuration.Value;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Reducer timeren med den tid, der er gået siden sidste frame
        timer -= Time.deltaTime;

        // Hvis timeren er udløbet, returner Success
        if (timer <= 0)
        {
            return Status.Success;
        }

        // Ellers returner Running for at indikere, at handlingen stadig er i gang
        return Status.Running;
    }
}

