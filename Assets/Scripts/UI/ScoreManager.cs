using System;
using System.Collections.Generic;
using AI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

	public TextMeshProUGUI Team1Text; 
	public TextMeshProUGUI Team2Text; 

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
		UpdateScores();
	}

	public void UpdateScores()
	{
		Team1Text.text = "Team 1: " + TeamPoints[1];
		Team2Text.text = "Team 2: " + TeamPoints[2];
	}
}