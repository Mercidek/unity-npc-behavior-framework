using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackState_", menuName = "NPC Behavior System/State/New Attack State")]
public class StateAttackSO : StateSO
{
    [SerializeField] private string attackAnimTrigger = "Attack";
    [SerializeField] private float attackRange = 4f;

    public override void EnterState(INPCBehavior behavior)
    {
        behavior.Stop();
        behavior.TriggerAnimation(attackAnimTrigger);

        Transform targetObject = behavior.GetTarget();
        if(targetObject == null) return;

        Vector3 targetDirection = targetObject.position - behavior.GetCurrentPosition();
        float sqrTargetDistance = targetDirection.sqrMagnitude;

        if(sqrTargetDistance <= attackRange * attackRange)
        {
            behavior.SendAttackSignal();
        }
    }

    public override void UpdateState(INPCBehavior behavior)
    {
        Transform targetObject = behavior.GetTarget();
        if(targetObject != null)
        {
            Vector3 targetPosition = targetObject.position;
            Vector3 myPosition = behavior.GetCurrentPosition();

            targetPosition.y = 0f;
            myPosition.y = 0f;

            Vector3 targetDirection = targetPosition - myPosition;
            float targetDistance    = targetPosition.magnitude;

            if(targetDirection != Vector3.zero)
            {
                behavior.RotateToDirection(targetDirection);
            }
        }
    }

    public override void ExitState(INPCBehavior behavior)
    {
    }
}
