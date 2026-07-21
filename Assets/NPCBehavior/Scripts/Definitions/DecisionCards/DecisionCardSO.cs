using UnityEngine;

public abstract class DecisionCardSO : ScriptableObject
{
    public abstract bool Decide(Transform aiTransform, float timeInState);
}