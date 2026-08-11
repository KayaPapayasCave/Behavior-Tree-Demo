using UnityEngine;

public class GuardAI : MonoBehaviour
{
    // Reference til Player transform
    [Header("Player")]
    public Transform player;

    // Reference til bevægelses hastighed
    [Header("Movement")]
    public float moveSpeed = 3f;

    // Reference til patrol points og index
    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int patrolIndex;

    // Reference til detection og alert status
    [Header("Detection")]
    public float detectionRange = 5f;
    public bool canSeePlayer = false;
    public bool isAlerted = false;

    // Reference til last known position
    [Header("Search")]
    public Vector2 lastKnownPosition;

    // Reference til alert radius
    [Header("Alert")]
    public float alertRadius = 8f;

    // Tegner detection og alert radius i editoren
    private void OnDrawGizmos()
    {
        // Tegner en gul cirkel for detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Tegner en rød cirkel for alert radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
    }
}