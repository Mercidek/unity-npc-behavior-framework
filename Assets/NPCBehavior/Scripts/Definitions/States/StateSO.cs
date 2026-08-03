using UnityEngine;

public abstract class StateSO : ScriptableObject
{
    public abstract void EnterState(INPCBehavior behavior);
    public abstract void UpdateState(INPCBehavior behavior);
    public abstract void ExitState(INPCBehavior behavior);
}
