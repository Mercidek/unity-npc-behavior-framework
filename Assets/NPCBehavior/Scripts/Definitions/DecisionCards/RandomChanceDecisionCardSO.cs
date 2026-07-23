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
}
