using UnityEngine;

[CreateAssetMenu(fileName = "NewPatrolState_", menuName = "NPC Behavior System/State/New Patrol State")]
public class StatePatrolSO : StateSO
{
    [SerializeField] private string patrolAnimBool = "isPatrolling";
    [SerializeField][Min(0.1f)] private float waypointTargetDistance = 0.5f;

    public override void EnterState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(patrolAnimBool, true);
    }

    public override void UpdateState(INPCBehavior behavior)
    {
        Transform currentWaypoint = behavior.GetCurrentWaypointPosition();
        Vector3 currentDirection  = currentWaypoint.position - behavior.GetCurrentPosition();
        currentDirection.y = 0f;

        float sqrDistance = currentDirection.sqrMagnitude;
        float sqrTargetDistance = waypointTargetDistance * waypointTargetDistance;
        float patrolSpeed = behavior.GetCurrentPatrolSpeed();

        if(currentWaypoint == null) return;

        if(sqrDistance <= sqrTargetDistance)
        {
            behavior.GoToNextWaypoint();
            return;
        }

        if(sqrDistance > Mathf.Epsilon)
        {
            behavior.RotateToDirection(currentDirection.normalized);
            behavior.MoveToPosition(currentWaypoint.position, patrolSpeed);
        }
    }

    public override void ExitState(INPCBehavior behavior)
    {
        behavior.PlayAnimation(patrolAnimBool, false);
    }
}
