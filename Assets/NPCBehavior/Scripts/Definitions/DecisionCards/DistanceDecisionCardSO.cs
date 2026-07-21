using UnityEngine;

[CreateAssetMenu(fileName = "NewDistanceDecisionCard_", menuName = "NPC Behavior System/State/Decision Card/New Distance Decision Card")]
public class DistanceDecisionCardSO : DecisionCardSO
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float minDistValue;
    [SerializeField] private float maxDistValue;

    public override bool Decide(Transform aiTransform, float timeInState)
    {
        if(targetObject == null) return false;
        float distance = Vector3.Distance(aiTransform.position, targetObject.transform.position);
        return distance >= minDistValue && distance <= maxDistValue;
    }
}
