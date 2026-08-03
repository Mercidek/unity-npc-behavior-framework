using UnityEngine;

public interface INPCBehavior
{
    void MoveToPosition(Vector3 targetPosition, float speed);
    void Stop();
    void PlayAnimation(string parameterName, bool value);
    void TriggerAnimation(string triggerName);
    Transform GetTarget();
    Vector3 GetCurrentPosition();
    float GetCurrentChaseSpeed();
}
