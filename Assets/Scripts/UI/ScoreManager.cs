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
    bool scoreActive = true;
    public Dictionary<int, int> TeamPoints = new Dictionary<int, int>();

    public EndMenu EndScreen;

    public void Awake()
    {
        Instance = this;
        TeamPoints.Add(1, 0);
        TeamPoints.Add(2, 0);
    }

    public void OnScored(Robot robot, int points)
    { 
      if(scoreActive == true)
        {
            OnScore();
            TeamPoints[robot.team] += points;
            UpdateScores();
        }
        else
        {
            return;
        }
	}

	public void UpdateScores()
	{
		Team1Text.text = "Team 1: " + TeamPoints[1];
		Team2Text.text = "Team 2: " + TeamPoints[2];
	}

    public void StopScores()
    {
        if (TeamPoints[1] == 5 && TeamPoints[2] == 5 )
        {
            scoreActive = false;
            Timer.Instance.timeActive = false;
            EndScreen.gameObject.SetActive(true);
        }
    }
}