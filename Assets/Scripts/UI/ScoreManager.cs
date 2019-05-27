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

    Dictionary<int, int> teamPoints = new Dictionary<int, int>();

    public void Awake()
    {
        Instance = this;
        teamPoints.Add(1, 0);
        teamPoints.Add(2, 0);
    }

    public void OnScored(Robot robot, int points)
    {
        //onRobotScored.Invoke(robot, points);
        teamPoints[robot.team] += points;
    }
}