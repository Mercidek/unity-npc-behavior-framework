using UnityEngine;
using System.Collections.Generic;

public class StateManager : MonoBehaviour
{
    [SerializeField] private StateSO initialState;
    [SerializeField] private GameObject targetObject;
    private StateSO currentState;
    private float timeInCurrentState = 0f;

    private AIContext aiContext;

    public List<StateTransition> transitions = new List<StateTransition>();

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
        aiContext.timeInState = timeInCurrentState;
    }
}

[System.Serializable]
public struct StateTransition
{
    public StateSO currentState;
    public StateSO targetState;
    public DecisionCardSO decisionCard;
}