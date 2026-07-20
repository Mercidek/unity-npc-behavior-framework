using UnityEngine;
using System.Collections.Generic;

public class StateManager : MonoBehaviour
{
    [SerializeField] private StateSO initialState;
    private StateSO currentState;

    public List<StateTransition> transitions = new List<StateTransition>();

    private void Start()
    {
        currentState = initialState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
        foreach(var transition in transitions)
        {
            if(transition.currentState == currentState && currentState.IsComplete(this))
            {
                SwitchState(transition.targetState);
                break;
            }
        }
    }

    public void SwitchState(StateSO state)
    {
        if(currentState != null) currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}

[System.Serializable]
public struct StateTransition
{
    public StateSO currentState;
    public StateSO targetState;
}