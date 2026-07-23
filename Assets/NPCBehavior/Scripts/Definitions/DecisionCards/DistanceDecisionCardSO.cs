using UnityEngine;

[CreateAssetMenu(fileName = "NewDistanceDecisionCard_", menuName = "NPC Behavior System/State/Decision Card/New Distance Decision Card")]
public class DistanceDecisionCardSO : DecisionCardSO
{
    [SerializeField] private float minDistValue;
    [SerializeField] private float maxDistValue;

    public override bool Decide(AIContext context)
    {
        if(context.targetObject == null) return false;
        float distance = Vector3.Distance(context.aiTransform.position, context.targetObject.transform.position);
        return distance >= minDistValue && distance <= maxDistValue;
    }
}
