using UnityEngine;
using UnityEngine.UI;
using AI;

public class ScoreText : MonoBehaviour
{
    public Text redText;
    public Text blueText;

    void Start()
    {
        ScoreManager.Instance.OnScore += () =>
        {
            redText.text = $"1: {ScoreManager.Instance.TeamPoints[1]}";
            blueText.text = $"2: {ScoreManager.Instance.TeamPoints[2]}";
        };
    }
}