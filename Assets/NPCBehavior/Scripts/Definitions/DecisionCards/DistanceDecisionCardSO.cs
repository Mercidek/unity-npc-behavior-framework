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

    public override void DrawCardGizmos(AIContext context)
    {
        #if UNITY_EDITOR
        Vector3 basePos       = context.aiTransform.position;
        float baseHeight      = 1.8f + context.debugYOffset;
        Vector3 labelPosition = basePos + (Vector3.up * baseHeight);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(basePos, Vector3.up, minDistValue);

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(basePos, Vector3.up, maxDistValue);

        string infoText = $"Distance Check\nMax: {maxDistValue}m\nMin: {minDistValue}m";
        UnityEditor.Handles.Label(labelPosition, infoText);

        context.debugYOffset += 2f;

        UnityEditor.Handles.color = Color.white;
        #endif
    }
}
