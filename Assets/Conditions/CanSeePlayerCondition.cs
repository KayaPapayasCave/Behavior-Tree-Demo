using System;
using Unity.Behavior;
using UnityEngine;

[Serializable]
[Unity.Properties.GeneratePropertyBag]
[Condition(name: "CanSeePlayer", story: "Can see Player?", category: "Conditions", id: "683c595dc4f0cfd8dc1e44ecbccfc2b8")]
public partial class CanSeePlayerCondition : Condition
{
    // Reference til GuardAI scriptet på GameObjectet
    private GuardAI guard;

    // Køres når Condition noden starter
    public override void OnStart()
    {
        guard = GameObject.GetComponent<GuardAI>();
    }

    // Evaluerer om vagten kan se spilleren
    public override bool IsTrue()
    {
        // Henter GuardAI-komponenten
        GuardAI guard = GameObject.GetComponent<GuardAI>();

        // Hvis GuardAI ikke findes, kan Conditionen ikke være opfyldt
        if (guard == null)
        {
            return false;
        }

        // Returnerer vagtens aktuelle canSeePlayer-værdi
        // true = spilleren kan ses
        // false = spilleren kan ikke ses
        return guard.canSeePlayer;
    }
}