using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private Transform playerTransform;
    private HealthSystem playerHealthSystem;
    private Dictionary<Type, AIState> availableStates;
    private AIState currentState;
    private float detectionRange = 3f;
    private Vector3[] patrolPoints;
    public GameObject[] patrolPointObjects;
    private int currentPatrolPointIndex = 0;
    private float attackRange = 2f;
    public float attackDamage = 20f;
    private bool isPlayerInRange;
    private Vector3 targetPosition;
    private float startDelay = 5f; // The AI will start checking if the player is in range after 5 seconds
    private float startTime;

    
    NavMeshAgent agent;
    public Animator anim;
    float currentSpeed;

    void Start()
    {
        startTime = Time.time;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealthSystem = playerTransform.GetComponent<HealthSystem>();
        patrolPoints = new Vector3[patrolPointObjects.Length];
        for (int i = 0; i < patrolPointObjects.Length; i++)
        {
            patrolPoints[i] = patrolPointObjects[i].transform.position;
        }
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        availableStates = new Dictionary<Type, AIState>()
        {
            { typeof(IdleState), new IdleState(this) },
            { typeof(PatrolState), new PatrolState(this) },
            { typeof(AttackState), new AttackState(this) }
        };

        TransitionToState(typeof(IdleState));
    }

    void Update()
    {
        if (Time.time - startTime > startDelay)
        {
            isPlayerInRange = IsPlayerInRange();
        }
        currentState.Execute?.Invoke();
        Type nextState = currentState.Transition();
        if (nextState != currentState.GetType())
        {
            TransitionToState(nextState);
        }

        if (currentState is AttackState && isPlayerInRange)
        {
            agent.SetDestination(playerTransform.position);
        }

        currentSpeed = Mathf.Lerp(currentSpeed, agent.velocity.magnitude, Time.deltaTime * 5);
        anim.SetFloat("Speed", currentSpeed);

    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= detectionRange;
    }

    public void TransitionToState(Type nextState)
    {
        if (currentState != null)
        {
            currentState.Exit?.Invoke();
        }
        currentState = availableStates[nextState];
        currentState.Enter?.Invoke();
        anim.SetBool("isIdle", currentState is IdleState);
        anim.SetBool("isPatrolling", currentState is PatrolState);
        anim.SetBool("isAttacking", currentState is AttackState);

    }
    public void Patrol()
    {
        if (targetPosition == null || Vector3.Distance(transform.position, targetPosition) < 0.5f) 
        {
            targetPosition = patrolPoints[UnityEngine.Random.Range(0, patrolPoints.Length)];
        }

        agent.SetDestination(targetPosition);
    }

    public void Attack()
    {
        if (isPlayerInRange && playerHealthSystem.GetCurrentHealth() > 0)
        {
            playerHealthSystem.TakeDamage(attackDamage);
        }
    }

    public void CheckPlayerDeath()
    {
        if (playerHealthSystem.GetCurrentHealth() <= 0)
        {
            Debug.Log("Enemy wins the fight");
        }
    }

    public bool ShouldPatrol()
    {
        return true;
    }
}