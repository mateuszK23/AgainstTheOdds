using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void setDifficulty(float value)
    {
        StateController.difficulty = value;
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
