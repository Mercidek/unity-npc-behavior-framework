using UnityEngine;

[CreateAssetMenu(fileName = "NewTimeDecisionCard_", menuName = "NPC Behavior System/State/Decision Card/New Time Decision Card")]
public class TimeDecisionCardSO : DecisionCardSO
{
    [SerializeField] private float minTimeInState;
    [SerializeField] private float maxTimeInState;

    public override bool Decide(Transform aiTransform, GameObject targetObject, float timeInState)
    {
        return timeInState >= minTimeInState && timeInState <= maxTimeInState;
    }
}
