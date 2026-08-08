using UnityEngine;

[CreateAssetMenu(fileName = "NewTimeDecisionCard_", menuName = "NPC Behavior System/State/Decision Card/New Time Decision Card")]
public class TimeDecisionCardSO : DecisionCardSO
{
    [SerializeField] private float minTimeInState;
    [SerializeField] private float maxTimeInState;

    public override bool Decide(AIContext context)
    {
        return context.timeInState >= minTimeInState && context.timeInState <= maxTimeInState;
    }

    public override void DrawCardGizmos(AIContext context)
    {
        #if UNITY_EDITOR
        Vector3 basePos       = context.aiTransform.position;
        float baseHeight      = 1.8f + context.debugYOffset;
        Vector3 labelPosition = basePos + (Vector3.up * baseHeight);

        float currentTimer = context.timeInState;

        string infoText = $"Time Check\nElapsed: {currentTimer:F1}s\nMin: {minTimeInState}s | Max: {maxTimeInState}s";
        UnityEditor.Handles.Label(labelPosition, infoText);

        context.debugYOffset += 2f;
        #endif
    }
}
