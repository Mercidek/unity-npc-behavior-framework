using UnityEngine;
using System.Collections.Generic;

public class AIContext
{
    public List<Transform> waypointList = new List<Transform>();

    public Transform aiTransform { get; private set; }
    public GameObject targetObject;
    public float timeInState;
    public float nextRollTime;
    public float viewThreshold = -2f;
    public float speedChase;
    public float speedFlee;
    public float speedPatrol;
    public float distanceFlee;
    public float delayDestroy;
    public int waypointIndex;

    // For Debug Visuals
    public float debugYOffset;

    public AIContext(Transform baseTransform)
    {
        aiTransform = baseTransform;
    }
}
