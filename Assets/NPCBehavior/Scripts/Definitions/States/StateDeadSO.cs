using UnityEngine;

[CreateAssetMenu(fileName = "NewDeadState_", menuName = "NPC Behavior System/State/New Dead State")]
public class StateDeadSO : StateSO
{
    [SerializeField] private string dieAnimTrigger = "Die";

    public override void EnterState(INPCBehavior behavior)
    {
        behavior.Stop();
        behavior.TriggerAnimation(dieAnimTrigger);
        behavior.CleanupAfterDeath();
    }

    public override void UpdateState(INPCBehavior behavior)
    {
    }

    public override void ExitState(INPCBehavior behavior)
    {
    }
}
