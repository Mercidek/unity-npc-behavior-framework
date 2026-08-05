using System;
using UnityEngine;

public interface INPCBehavior
{
    event Action OnAttackTriggered;

    void MoveToPosition(Vector3 targetPosition, float speed);
    void Stop();
    void PlayAnimation(string parameterName, bool value);
    void TriggerAnimation(string triggerName);
    void CleanupAfterDeath();
    void LookAtTarget(Transform target);
    void RotateToDirection(Vector3 direction);
    Transform GetTarget();
    Vector3 GetCurrentPosition();
    Transform GetCurrentWaypointPosition();
    void GoToNextWaypoint();
    void SendAttackSignal();
    float GetCurrentChaseSpeed();
    float GetCurrentFleeSpeed();
    float GetCurrentPatrolSpeed();
    float GetFleeDistance();
}
