using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public void Start()
    {
        var text = GetComponent<Text>();
        ScoreManager.Instance.onRobotScored.AddListener((team, points) =>
        {
            text.text = $"{team.name}: {points}";
        });
    }
}