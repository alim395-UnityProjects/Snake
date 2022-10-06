using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    private int score = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void ResetPoint()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
