using UnityEngine;
using System.Collections.Generic;

public class StateManager : MonoBehaviour
{
    [SerializeField] private StateSO initialState;
    private StateSO currentState;
    private float timeInCurrentState = 0f;

    public List<StateTransition> transitions = new List<StateTransition>();

    private void Start()
    {
        currentState = initialState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        timeInCurrentState += Time.deltaTime;
        currentState.UpdateState(this);
        foreach(var transition in transitions)
        {
            if(transition.currentState == currentState)
            {
                if(transition.decisionCard != null && transition.decisionCard.Decide(transform, timeInCurrentState))
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
        currentState.EnterState(this);
    }
}

[System.Serializable]
public struct StateTransition
{
    public StateSO currentState;
    public StateSO targetState;
    public DecisionCardSO decisionCard;
}