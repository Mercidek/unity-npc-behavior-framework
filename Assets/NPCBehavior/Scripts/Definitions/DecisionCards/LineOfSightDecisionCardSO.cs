using UnityEngine;

[CreateAssetMenu(fileName = "NewLineOfSightDecisionCard_", menuName = "NPC Behavior System/State/Decision Card/New Line Of Sight Decision Card")]
public class LineOfSightDecisionCardSO : DecisionCardSO
{
    [SerializeField][Range(0.01f, 180f)] private float viewAngle = 45f;
    [SerializeField] private LayerMask obstacleMask;

    public override bool Decide(AIContext context)
    {
        if(context.targetObject == null) return false;
        if(obstacleMask.value == 0) return false;

        if (context.viewThreshold < -1f)
        {
            float halfAngle = viewAngle * 0.5f;
            context.viewThreshold = Mathf.Cos(halfAngle * Mathf.Deg2Rad);
        }

        Vector3 lookAt = context.targetObject.transform.position - context.aiTransform.position;
        Vector3 lookDirection = lookAt.normalized;
        float dotResult = Vector3.Dot(context.aiTransform.forward, lookDirection);

        if(dotResult < context.viewThreshold) return false;

        float maxDist = lookAt.magnitude;
        if(Physics.Raycast(context.aiTransform.position, lookDirection, maxDist, obstacleMask))
        {
            return false;
        }

        return true;
    }
}
