using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public void endGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}
