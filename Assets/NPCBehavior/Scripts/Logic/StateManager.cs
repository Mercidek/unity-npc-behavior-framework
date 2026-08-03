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
    [SerializeField] private float chaseSpeed = 2f;

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
}

[System.Serializable]
public struct StateTransition
{
    public StateSO currentState;
    public StateSO targetState;
    public DecisionCardSO decisionCard;
}