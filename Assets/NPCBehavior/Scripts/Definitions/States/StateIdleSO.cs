using UnityEngine;

[CreateAssetMenu(fileName = "NewIdleState_", menuName = "NPC Behavior System/State/New Idle State")]
public class StateIdleSO : StateSO
{
    [SerializeField] private string idleAnimBool = "isIdle";

    public override void EnterState(INPCBehavior behavior)
    {
        behavior.Stop();
        behavior.PlayAnimation(idleAnimBool, true);
    }

    public override void UpdateState(INPCBehavior behavior)
    {
    }

    public override void ExitState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(idleAnimBool, false);
    }
}
