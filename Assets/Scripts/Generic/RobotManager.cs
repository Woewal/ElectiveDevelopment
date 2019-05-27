using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System;

public class RobotManager : MonoBehaviour
{
    public static RobotManager Instance { get; private set; }

    public List<Robot> allRobots { get; private set; } = new List<Robot>();

    public Action<Robot> OnRobotAdded = (robot) => { };

    private int currentId;

    public void Awake()
    {
        Instance = this;
    }

    public void Register(Robot robot)
    {
        robot.id = currentId++;
        allRobots.Add(robot);
        OnRobotAdded(robot);
    }
}
