using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsAlertedByOtherGuards", story: "Is alerted?", category: "Conditions", id: "862b67b516b403dc9e27f42b5d61623c")]
public partial class IsAlertedByOtherGuardsCondition : Condition
{
    // Blackboard variabel
    [SerializeReference]
    public BlackboardVariable<GameObject> Guard;

    // Evaluerer om vagten er blevet alarmeret af andre vagter
    public override bool IsTrue()
    {
        // Henter GuardAI komponenten fra den aktuelle Guard
        GuardAI guard = GameObject.GetComponent<GuardAI>();

        // Hvis GuardAI ikke findes, returner false
        if (guard == null)
            return false;

        // Returnerer vagtens aktuelle alarmtilstand
        // true = vagten er alarmeret
        // false = vagten er ikke alarmeret
        return guard.isAlerted;
    }
}