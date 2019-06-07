using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinText : MonoBehaviour
{
    public Text finishText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.OnScore += () =>
        {
            if (TeamPoints[1] == 100)
            {
                finishText.text = "Winner: Team 1";
            }
            else if (TeamPoints[2] == 100)
            {
                finishText.text = "Winner: Team 2";
            }
        };
    }

 
}
