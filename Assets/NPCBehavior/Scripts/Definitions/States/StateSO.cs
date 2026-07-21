using UnityEngine;

public abstract class StateSO : ScriptableObject
{
    public abstract void EnterState(StateManager manager);
    public abstract void UpdateState(StateManager manager);
    public abstract void ExitState(StateManager manager);
}
