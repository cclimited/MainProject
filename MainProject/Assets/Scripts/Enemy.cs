using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float Health;

    [Header("Enemy Settings")]
    public Transform target;

    [Header("Idle Settings")]
    public EnemyStates state = EnemyStates.Idle;
    public Vector3 startingPosition;

    [Header("Attack Settings")]
    public float attackDistance = 2;

    [Header("Hungting Settings")]
    public int huntTime = 5;
    float endHuntTime = 0;

    [Header("Sight Settings")]
    public bool canSeePlayer;
    public Transform eyes;
    public int sightRange;
    public LayerMask sightBlockers;


    [Header("Components")]
    public NavMeshAgent navAgent;
    public Animator animator;

    void Start()
    {
        navAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        startingPosition = this.transform.position;
        target = PlayerMovement.instance.transform;
    }


    void Update()
    {
        canSeePlayer = CheckForPlayer();
        HandleStates();
        HandleAnimation();
    }

    void HandleAnimation()
    {
        float speedNorm = navAgent.velocity.magnitude / navAgent.speed;
        animator.SetFloat("Speed", speedNorm);

        float distanceToTarget = Vector3.Distance(this.transform.position, target.position);
        if (distanceToTarget <= attackDistance)
            animator.SetTrigger("Attack");
    }

    void HandleStates()
    {
        switch (state)
        {
            case EnemyStates.Idle:
                if (canSeePlayer)
                    state = EnemyStates.Chasing;
                break;

            case EnemyStates.Chasing:
                navAgent.destination = target.position;
                if (!canSeePlayer)
                {
                    state = EnemyStates.Hunting;
                    endHuntTime = Time.time + huntTime;
                }
                break;

            case EnemyStates.Hunting:
                if (canSeePlayer)
                    state = EnemyStates.Chasing;
                else if (Time.time > endHuntTime)
                {
                    state = EnemyStates.Idle;
                    navAgent.destination = startingPosition;
                }
                break;
        }
    }

    bool CheckForPlayer()
    {
        //check to see if target is close enough to be seen
        float distanceToTarget = Vector3.Distance(this.transform.position, target.position);
        if (distanceToTarget > sightRange)
            return false;

        //now check for obstructions
        if (Physics.Linecast(eyes.position, target.position, sightBlockers))
            return false;

        //target is in range, and not obstructed.
        return true;
    }

    public void TakeDamage(float amount)
    {
        animator.SetTrigger("Hit");
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
        }

    }

    void Die()
    {
        animator.SetTrigger("Death");
        Destroy(this.gameObject);
    }
}



public enum EnemyStates
{
    Idle,
    Chasing,
    Attack,
    Hunting
}
