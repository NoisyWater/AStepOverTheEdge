using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class BossEnemy : MonoBehaviour
{
    public RandomSoundPlayer soundPlayer;

    public int health = 500; // stronger boss

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bloom;
    public float fireRate;
    private float lastShotTime = 0f;

    private Rigidbody rb;

    // WIN UI
    public WinUI winUI;

    // AI
    public int currentPointIndex = 0;
    public Vector3 currentTarget;
    public float positionThreshold;
    public float idleTime = 5f;
    public float attackDistance = 5f;
    public float maxVisionDistance = 20f;
    public float minChasingHealth = 30f;

    public Transform[] patrolPoints;
    private float idleTimeCounter;
    private Transform playerTransform;
    private bool canSeePlayer;
    private Vector3 lastKnownPlayerPosition;

    private NavMeshAgent agent;

    public enum State { Idle, Patrolling, Chasing, Attacking }
    public State state = State.Idle;

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;

        GameObject patrolPointParent = GameObject.FindWithTag("PatrolPoint");
        patrolPoints = patrolPointParent.GetComponentsInChildren<Transform>()
            .Where(t => t != patrolPointParent.transform).ToArray();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Damage"))
        {
            health -= 10;

            if (soundPlayer != null)
                soundPlayer.PlayRandomEDamageSound();

            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        agent.enabled = false;

        if (winUI != null)
        {
            winUI.ShowWin();
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        if (isDead) return;

        LookForPlayer();

        switch (state)
        {
            case State.Idle: Idle(); break;
            case State.Patrolling: Patrolling(); break;
            case State.Attacking: Attacking(); break;
            case State.Chasing: Chasing(); break;
        }

        rb.linearVelocity = Vector3.zero;

        SetLastKnownPlayerPosition();
    }

    private void LookForPlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, maxVisionDistance))
        {
            canSeePlayer = hit.transform == playerTransform;

            if (canSeePlayer && state != State.Attacking)
                state = State.Chasing;
        }
    }

    private void Idle()
    {
        agent.ResetPath();
        idleTimeCounter -= Time.deltaTime;

        if (idleTimeCounter < 0)
        {
            state = State.Patrolling;
            idleTimeCounter = idleTime;
        }
    }

    private void Patrolling()
    {
        if (Vector3.Distance(currentTarget, transform.position) < positionThreshold)
        {
            currentPointIndex++;
            currentTarget = patrolPoints[currentPointIndex % patrolPoints.Length].position;
        }
        else
        {
            agent.SetDestination(currentTarget);
        }
    }

    private void Attacking()
    {
        agent.ResetPath();
        Shoot();

        if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
            state = State.Chasing;
    }

    private void Chasing()
    {
        agent.SetDestination(lastKnownPlayerPosition);
    }

    private void SetLastKnownPlayerPosition()
    {
        if (canSeePlayer)
            lastKnownPlayerPosition = playerTransform.position;
    }

    private void Shoot()
    {
        if (Time.time > lastShotTime + fireRate)
        {
            Vector3 dir = (playerTransform.position - transform.position).normalized;

            Quaternion rot = Quaternion.LookRotation(dir);

            Instantiate(bulletPrefab, bulletSpawnPoint.position, rot);

            lastShotTime = Time.time;
        }
    }
}