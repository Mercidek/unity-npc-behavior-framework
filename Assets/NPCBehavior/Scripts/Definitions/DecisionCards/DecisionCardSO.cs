using UnityEngine;

public abstract class DecisionCardSO : ScriptableObject
{
    public abstract bool Decide(Transform aiTransform, GameObject targetObject, float timeInState);
}