using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class StateManager : MonoBehaviour, INPCBehavior
{
    [SerializeField] private StateSO initialState;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    private StateSO currentState;
    private float timeInCurrentState = 0f;

    [SerializeField] private float chaseSpeed   = 2f;
    [SerializeField] private float fleeSpeed    = 4f;
    [SerializeField] private float turnSpeed    = 2f;
    [SerializeField] private float patrolSpeed  = 1f;
    [SerializeField] private float fleeDistance = 10f;
    [SerializeField] private float destroyDelay = 4f;

    public event Action OnAttackTriggered;

    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    private AIContext aiContext;

    public List<StateTransition> transitions = new List<StateTransition>();

    private void Awake()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        OnAttackTriggered += ApplyDamageToTarget;
    }

    private void Start()
    {
        aiContext = new AIContext(transform);

        if(waypoints != null && waypoints.Count > 0)
        {
            foreach(var wp in waypoints)
            {
                if(wp != null)
                {
                    aiContext.waypointList.Add(wp);
                }
            }
        }

        SwitchState(initialState);
    }

    private void Update()
    {
        timeInCurrentState += Time.deltaTime;
        UpdateContext();
        currentState.UpdateState(this);
        foreach(var transition in transitions)
        {
            if(transition.currentState == currentState)
            {
                if(transition.decisionCard != null && transition.decisionCard.Decide(aiContext))
                {
                    SwitchState(transition.targetState);
                    break;
                }
            }
        }
    }

    private void OnDestroy()
    {
        OnAttackTriggered -= ApplyDamageToTarget;
    }

    // Debug Visuals
    private void OnDrawGizmos()
    {
        if(aiContext == null || currentState == null || transitions == null) return;

        aiContext.debugYOffset = 0f;

        foreach(var transition in transitions)
        {
            if(transition.currentState == currentState)
            {
                transition.decisionCard.DrawCardGizmos(aiContext);
            }
        }
    }

    public void SwitchState(StateSO state)
    {
        if(currentState != null) currentState.ExitState(this);
        currentState = state;
        timeInCurrentState = 0f;
        aiContext.nextRollTime = -1f;
        currentState.EnterState(this);
    }

    public void UpdateContext()
    {
        aiContext.targetObject = targetObject;
        aiContext.timeInState  = timeInCurrentState;
        aiContext.speedChase   = chaseSpeed;
        aiContext.speedFlee    = fleeSpeed;
        aiContext.speedPatrol  = patrolSpeed;
        aiContext.distanceFlee = fleeDistance;
        aiContext.delayDestroy = destroyDelay;
    }

    public Transform GetCurrentWaypointPosition()
    {
        if(aiContext.waypointList == null || aiContext.waypointList.Count == 0) return transform;

        if(aiContext.waypointIndex >= aiContext.waypointList.Count)
        {
            aiContext.waypointIndex = 0;
        }

        int index = aiContext.waypointIndex;
        return aiContext.waypointList[index];
    }

    public void GoToNextWaypoint()
    {
        if(aiContext.waypointList == null || aiContext.waypointList.Count == 0) return;

        aiContext.waypointIndex += 1;

        if(aiContext.waypointIndex >= aiContext.waypointList.Count)
        {
            aiContext.waypointIndex = 0;
        }
    }

    private void ApplyDamageToTarget()
    {
    }

    // Behaviors
    public void MoveToPosition(Vector3 targetPosition, float speed)
    {
        if(agent == null) return;
        agent.isStopped = false;
        agent.speed     = speed;
        agent.SetDestination(targetPosition);
    }

    public void Stop()
    {
        if(agent == null) return;
        agent.isStopped = true;
    }

    public void PlayAnimation(string parameterName, bool value)
    {
        if(animator == null) return;
        animator.SetBool(parameterName, value);
    }

    public void TriggerAnimation(string triggerName)
    {
        if(animator == null) return;
        animator.SetTrigger(triggerName);
    }

    public void CleanupAfterDeath()
    {
        Destroy(gameObject, destroyDelay);
    }

    public void LookAtTarget(Transform target)
    {
        Vector3 targetDirection = target.position - transform.position;
        RotateToDirection(targetDirection);
    }

    public void RotateToDirection(Vector3 direction)
    {
        if(direction == Vector3.zero) return;

        direction.y = 0f;
        Quaternion targetDirection = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, turnSpeed * Time.deltaTime);
    }

    public Transform GetTarget()
    {
        return targetObject.transform;
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    public void SendAttackSignal()
    {
        OnAttackTriggered?.Invoke();
    }

    public float GetCurrentChaseSpeed()
    {
        return chaseSpeed;
    }

    public float GetCurrentFleeSpeed()
    {
        return fleeSpeed;
    }

    public float GetCurrentPatrolSpeed()
    {
        return patrolSpeed;
    }

    public float GetFleeDistance()
    {
        return fleeDistance;
    }
}

[System.Serializable]
public struct StateTransition
{
    public StateSO currentState;
    public StateSO targetState;
    public DecisionCardSO decisionCard;
}