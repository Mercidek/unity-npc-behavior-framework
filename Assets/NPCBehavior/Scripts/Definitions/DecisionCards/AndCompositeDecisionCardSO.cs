using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewAndDecisionCard_", menuName = "NPC Behavior System/State/Decision Card/New 'AND' Composite Decision Card")]
public class AndCompositeDecisionCardSO : DecisionCardSO
{
    [SerializeField] private List<DecisionCardSO> decisions = new List<DecisionCardSO>();
    public override bool Decide(AIContext context)
    {
        foreach(var decision in decisions)
        {
            if(!decision.Decide(context)) return false;
        }
        return true;
    }
}
