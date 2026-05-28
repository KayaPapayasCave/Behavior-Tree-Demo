using UnityEngine;

// Skal fungere som AI'ens hukommelse
public class GuardAI : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int patrolIndex;

    [Header("Detection")]
    public bool canSeePlayer;
    public bool isAlerted;

    [Header("Search")]
    public Vector2 lastKnownPosition;

    [Header("Alert")]
    public float alertRadius = 8f;
}
