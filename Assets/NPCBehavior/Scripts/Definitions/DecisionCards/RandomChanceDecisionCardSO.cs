using UnityEngine;

[CreateAssetMenu(fileName = "NewRandomChanceDecisionCard_", menuName = "NPC Behavior System/State/Decision Card/New Random Chance Decision Card")]
public class RandomChanceDecisionCardSO : DecisionCardSO
{
    [SerializeField][Range(0f, 1f)] private float successRate = 0.5f;
    [SerializeField][Min(0.01f)] private float rollCooldown;

    public override bool Decide(AIContext context)
    {
        if(context.nextRollTime == -1f) context.nextRollTime = rollCooldown;

        if(context.timeInState >= context.nextRollTime)
        {
            context.nextRollTime = context.timeInState + rollCooldown;
            return Random.value < successRate;
        }
        return false;
    }

    public override void DrawCardGizmos(AIContext context)
    {
        #if UNITY_EDITOR
        Vector3 basePos       = context.aiTransform.position;
        float baseHeight      = 1.8f + context.debugYOffset;
        Vector3 labelPosition = basePos + (Vector3.up * baseHeight);

        float currentTimer = context.timeInState;
        float nextRollTime = context.nextRollTime;

        string infoText = $"Random Chance Check\nElapsed: {currentTimer:F1}s\nNext Roll Time: {nextRollTime:F1}s\nSuccess Rate: {(successRate * 100f):F0}%";
        UnityEditor.Handles.Label(labelPosition, infoText);

        context.debugYOffset += 2.6f;

        UnityEditor.Handles.color = Color.white;
        #endif
    }
}
