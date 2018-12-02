using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static int Score;
     Text ScoreText;

    private void Start()
    {
        ScoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    public void IncreaseScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
    }
}
