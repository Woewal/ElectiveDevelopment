using System;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    //public class EventPointsScored : UnityEvent<Robot, int> {}

    //public EventPointsScored onRobotScored = new EventPointsScored();

    public UnityAction OnScore = delegate { };

    public Dictionary<int, int> TeamPoints = new Dictionary<int, int>();

    public void Awake()
    {
        Instance = this;
        TeamPoints.Add(1, 0);
        TeamPoints.Add(2, 0);
    }

    public void OnScored(Robot robot, int points)
    {
        OnScore();
        TeamPoints[robot.team] += points;
    }
}