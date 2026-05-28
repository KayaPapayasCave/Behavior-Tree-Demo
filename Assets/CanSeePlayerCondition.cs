using System;
using Unity.Behavior;
using UnityEngine;

[Serializable]
[Unity.Properties.GeneratePropertyBag]
[Condition(name: "CanSeePlayer", story: "Player is within sight of the Guard", category: "Conditions", id: "683c595dc4f0cfd8dc1e44ecbccfc2b8")]
public partial class CanSeePlayerCondition : Condition
{
    public override bool IsTrue()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject guard = GameObject.FindWithTag("Guard");

        if (player == null || guard == null)
            return false;

        float distance = Vector2.Distance(
            player.transform.position,
            guard.transform.position
        );

        return distance <= 5f;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}