using UnityEngine;

public interface INPCBehavior
{
    void MoveToPosition(Vector3 targetPosition, float speed);
    void Stop();
    void PlayAnimation(string parameterName, bool value);
    void TriggerAnimation(string triggerName);
    void CleanupAfterDeath();
    void LookAtTarget(Transform target);
    void RotateToDirection(Vector3 direction);
    Transform GetTarget();
    Vector3 GetCurrentPosition();
    float GetCurrentChaseSpeed();
    float GetCurrentFleeSpeed();
    float GetFleeDistance();
}
