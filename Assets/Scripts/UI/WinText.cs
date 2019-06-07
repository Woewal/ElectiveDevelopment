using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinText : MonoBehaviour
{
    public TextMeshProUGUI finishText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.OnScore += () =>
        {
            if (ScoreManager.Instance.TeamPoints[1] ==100)
            {
                finishText.text = "Winner: Team 1";
                finishText.color = Color.red;
            }
            else if (ScoreManager.Instance.TeamPoints[2] == 100)
            {
                finishText.text = "Winner: Team 2";
                finishText.color = Color.blue;
            }
        };
    }

 
}
