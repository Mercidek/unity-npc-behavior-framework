using UnityEngine;

public abstract class DecisionCardSO : ScriptableObject
{
    public abstract bool Decide(AIContext context);

    public virtual void DrawCardGizmos(AIContext context)
    {
    }
}