using UnityEngine;

public class StateMachineEksempel : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Investigate,
        Search
    }

    [Header("Player")]
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int patrolIndex;

    [Header("Detection")]
    public float detectionRange = 5f;
    public bool canSeePlayer;

    [Header("Alert")]
    public bool isAlerted;
    public float alertRadius = 8f;

    [Header("Search")]
    public Vector2 lastKnownPosition;
    public float searchDuration = 3f;

    private float searchTimer;

    private State currentState = State.Patrol;


    private void Update()
    {
        // Detection kører hele tiden
        CheckPlayerDetection();

        // Vælg hvad vagten skal gøre
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                ChasePlayer();
                break;

            case State.Investigate:
                MoveToLastKnownPosition();
                break;

            case State.Search:
                SearchArea();
                break;
        }

        // Bestem om state skal ændres
        UpdateState();
    }


    // -------------------------
    // DETECTION
    // -------------------------

    private void CheckPlayerDetection()
    {
        if (player == null)
            return;

        float distance =
            Vector2.Distance(transform.position, player.position);

        canSeePlayer = distance <= detectionRange;

        if (canSeePlayer)
        {
            lastKnownPosition = player.position;
        }
    }


    // -------------------------
    // STATE TRANSITIONS
    // -------------------------

    private void UpdateState()
    {
        switch (currentState)
        {
            case State.Patrol:

                if (canSeePlayer)
                {
                    AlertNearbyGuards();

                    currentState = State.Chase;
                }
                else if (isAlerted)
                {
                    currentState = State.Investigate;
                }

                break;


            case State.Chase:

                if (!canSeePlayer)
                {
                    currentState = State.Investigate;
                }

                break;


            case State.Investigate:

                if (canSeePlayer)
                {
                    currentState = State.Chase;
                }
                else if (ReachedLastKnownPosition())
                {
                    searchTimer = searchDuration;
                    currentState = State.Search;
                }

                break;


            case State.Search:

                if (canSeePlayer)
                {
                    currentState = State.Chase;
                }
                else if (searchTimer <= 0)
                {
                    isAlerted = false;
                    currentState = State.Patrol;
                }

                break;
        }
    }


    // -------------------------
    // PATROL
    // -------------------------

    private void Patrol()
    {
        if (patrolPoints == null ||
            patrolPoints.Length == 0)
            return;

        Transform target =
            patrolPoints[patrolIndex];

        transform.position =
            Vector3.MoveTowards(
                transform.position,
                target.position,
                moveSpeed * Time.deltaTime
            );

        if (Vector3.Distance(
                transform.position,
                target.position) < 0.2f)
        {
            patrolIndex =
                (patrolIndex + 1)
                % patrolPoints.Length;
        }
    }


    // -------------------------
    // CHASE
    // -------------------------

    private void ChasePlayer()
    {
        if (player == null)
            return;

        transform.position =
            Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
    }


    // -------------------------
    // INVESTIGATE
    // -------------------------

    private void MoveToLastKnownPosition()
    {
        transform.position =
            Vector2.MoveTowards(
                transform.position,
                lastKnownPosition,
                moveSpeed * Time.deltaTime
            );
    }


    private bool ReachedLastKnownPosition()
    {
        return Vector2.Distance(
            transform.position,
            lastKnownPosition) < 0.1f;
    }


    // -------------------------
    // SEARCH
    // -------------------------

    private void SearchArea()
    {
        searchTimer -= Time.deltaTime;
    }


    // -------------------------
    // ALERT OTHER GUARDS
    // -------------------------

    private void AlertNearbyGuards()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                transform.position,
                alertRadius
            );

        foreach (Collider2D hit in hits)
        {
            StateMachineEksempel otherGuard =
                hit.GetComponent<StateMachineEksempel>();

            if (otherGuard != null &&
                otherGuard != this)
            {
                otherGuard.isAlerted = true;
                otherGuard.lastKnownPosition =
                    lastKnownPosition;
            }
        }
    }
}
