using UnityEngine;

[CreateAssetMenu(fileName = "NewFleeState_", menuName = "NPC Behavior System/State/New Flee State")]
public class StateFleeSO : StateSO
{
    [SerializeField] private string fleeAnimBool = "isFleeing";

    public override void EnterState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(fleeAnimBool, true);
    }

    public override void UpdateState(INPCBehavior behavior)
    {
        Transform targetObject = behavior.GetTarget();
        if(targetObject != null)
        {
            Vector3 targetPosition = targetObject.position;
            Vector3 myPosition     = behavior.GetCurrentPosition();

            targetPosition.y = 0f;
            myPosition.y     = 0f;

            Vector3 targetDirection = myPosition - targetPosition;

            Vector3 moveTargetPos = myPosition + targetDirection.normalized * behavior.GetFleeDistance();
            float fleeSpeed = behavior.GetCurrentFleeSpeed();
            if(targetDirection != Vector3.zero)
            {
                behavior.RotateToDirection(targetDirection);
                behavior.MoveToPosition(moveTargetPos, fleeSpeed);
            }
        }
    }

    public override void ExitState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(fleeAnimBool, false);
    }
}
