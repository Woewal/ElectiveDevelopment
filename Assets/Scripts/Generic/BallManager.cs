using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }

    public Transform ballTransform { get; private set; }
	public Robot assignedPlayer;

    public void Awake()
    {
        Instance = this;
    }

    public void Register(GameObject ball)
    {
        ballTransform = ball.transform;
    }
}
