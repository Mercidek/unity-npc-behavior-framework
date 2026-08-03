using UnityEngine;

[CreateAssetMenu(fileName = "NewChaseState_", menuName = "NPC Behavior System/State/New Chase State")]
public class StateChaseSO : StateSO
{
    [SerializeField] private string chaseAnimBool = "isChasing";

    public override void EnterState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(chaseAnimBool, true);
    }

    public override void UpdateState(INPCBehavior behavior)
    {
        Transform targetObject = behavior.GetTarget();
        if(targetObject != null)
        {
            float chaseSpeed = behavior.GetCurrentChaseSpeed();
            behavior.MoveToPosition(targetObject.position, chaseSpeed);
        }
    }

    public override void ExitState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(chaseAnimBool, false);
    }
}
