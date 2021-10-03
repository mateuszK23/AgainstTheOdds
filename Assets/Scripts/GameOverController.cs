using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MenuController
{
    [SerializeField]
    private Text scoreTextField;
    [SerializeField]
    private Text bestScoreTextField;
    private void Start()
    {
        int highScore;
        if (StateController.score > StateController.highestScore)
        {
            StateController.highestScore = StateController.score;
            highScore = StateController.score;
        }
        else highScore = StateController.highestScore;
        scoreTextField.text = "Score: " + StateController.score.ToString();
        bestScoreTextField.text = "Highest Score: " + highScore;
    }
}
