using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

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
    [SerializeField] private float fleeDistance = 10f;
    [SerializeField] private float destroyDelay = 4f;

    private AIContext aiContext;

    public List<StateTransition> transitions = new List<StateTransition>();

    private void Awake()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        aiContext = new AIContext(transform);
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
        aiContext.distanceFlee = fleeDistance;
        aiContext.delayDestroy = destroyDelay;
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

    public float GetCurrentChaseSpeed()
    {
        return chaseSpeed;
    }

    public float GetCurrentFleeSpeed()
    {
        return fleeSpeed;
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