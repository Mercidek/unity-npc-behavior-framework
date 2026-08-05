using UnityEngine;

[CreateAssetMenu(fileName = "NewPatrolState_", menuName = "NPC Behavior System/State/New Patrol State")]
public class StatePatrolSO : StateSO
{
    [SerializeField] private string patrolAnimBool = "isPatrolling";

    public override void EnterState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(patrolAnimBool, true);
    }

    public override void UpdateState(INPCBehavior behavior)
    {
        Transform currentWaypoint = behavior.GetCurrentWaypointPosition();
        Vector3 currentDirection  = currentWaypoint.position - behavior.GetCurrentPosition();
        float sqrDistance = currentDirection.sqrMagnitude;
        float patrolSpeed = behavior.GetCurrentPatrolSpeed();

        if(sqrDistance <= 0.5f)
        {
            behavior.GoToNextWaypoint();
        }

        if(sqrDistance > 0.01f)
        {
            behavior.RotateToDirection(currentDirection);
            behavior.MoveToPosition(currentWaypoint.position, patrolSpeed);
        }
    }

    public override void ExitState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(patrolAnimBool, false);
    }
}
