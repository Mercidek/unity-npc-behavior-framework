using UnityEngine;

[CreateAssetMenu(fileName = "NewIdleState_", menuName = "NPC Behavior System/State/New Idle State")]
public class StateIdleSO : StateSO
{
    [SerializeField] private float maxIdleTime = 4f;
    private float currentIdleTimer;

    public override void EnterState(StateManager manager)
    {
        currentIdleTimer = 0f;
    }

    public override void UpdateState(StateManager manager)
    {
        currentIdleTimer += Time.deltaTime;
    }

    public override void ExitState(StateManager manager)
    {
    }
    public override bool IsComplete(StateManager manager)
    {
        return currentIdleTimer >= maxIdleTime;
    }
}
