using UnityEngine;

public class StateManager : MonoBehaviour
{
    StateSO currentState;

    private void Start()
    {
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(StateSO state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}