using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI GameTime;
    public float timeStart;

    public bool timeActive = true;

    public static Timer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActive == true)
        {
            float t = Time.time;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("F0");

            GameTime.text = minutes + ":" + seconds;
        }
        else
        {
            GameTime.color = Color.yellow;
        }
    }
}
