using System;
using AI;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public class EventPointsScored : UnityEvent<Robot, int> { }

    public EventPointsScored onRobotScored = new EventPointsScored();


    public void Awake()
    {
        Instance = this;
       
    }

    public void OnScored(Robot team, int points)
    {
        onRobotScored.Invoke(team, points);
    }
}