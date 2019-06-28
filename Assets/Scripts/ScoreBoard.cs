using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    //hurray for changes!
    int score = 0;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreHit(int scorePerHit)
    {
        score += scorePerHit;
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = score.ToString();
    }
}
