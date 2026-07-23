using UnityEngine;

public class AIContext
{
    public Transform aiTransform { get; private set; }
    public GameObject targetObject;
    public float timeInState;
    public float nextRollTime;

    public AIContext(Transform baseTransform)
    {
        aiTransform = baseTransform;
    }
}
