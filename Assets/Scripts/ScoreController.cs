using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private Text scoreTextField;
    private void Start()
    {
        scoreTextField = GetComponent<Text>();
        StateController.score = 0;
        readScore();
    }
    private void Update()
    {
        readScore();
    }
    private void readScore()
    {
        scoreTextField.text = "Score: " + StateController.score.ToString();
    }
}
