using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManger : MonoBehaviour
{
    public Text redText;
    public Text blueText;

    public float scoreCountRed;
    public float scoreCountBlue;
    public float pointsPerSecond;

    public bool scoreIncreasingRed;
    public bool scoreIncreasingBlue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasingRed)
        {
            scoreCountRed += pointsPerSecond * Time.deltaTime;
        }
        if (scoreIncreasingBlue)
        {
            scoreCountBlue += pointsPerSecond * Time.deltaTime;
        }
        redText.text = "Red team  " + Mathf.Round (scoreCountRed);
        blueText.text = Mathf.Round(scoreCountBlue) + "  Blue team";
    }
}
