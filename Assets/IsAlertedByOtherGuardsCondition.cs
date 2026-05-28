using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsAlertedByOtherGuards", story: "Guard is alerted by other Guards", category: "Conditions", id: "862b67b516b403dc9e27f42b5d61623c")]
public partial class IsAlertedByOtherGuardsCondition : Condition
{
    public BlackboardVariable<bool> IsAlerted;

    public override bool IsTrue()
    {
        return IsAlerted.Value;
    }

    public override void OnStart() { }

    public override void OnEnd() { }
}