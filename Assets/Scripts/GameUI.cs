using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    int score;

    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //int c = 3 + 1;
        //float a = 1.234f + 2.1f;
        //Debug.Log(a);
        //string b = "1 + 1";
        scoreText.text = score.ToString();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score = score + 1;
        scoreText.text = score.ToString();
    }
}
