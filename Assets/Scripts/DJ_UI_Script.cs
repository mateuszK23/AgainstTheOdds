using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DJ_UI_Script : MonoBehaviour
{
    private Text textField;
    private PlayerScript player;
    private bool DJ_enabled;
    private float targetTime;
    private float remainingTime;
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerScript>();
        textField = GetComponent<Text>();
        targetTime = 11.0f;
        remainingTime = 0;
    }

    void Start()
    {
        textField.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        CoinRead();
    }

    private void CoinRead()
    {
        DJ_enabled = player.djEnabled;

        if (player.DJ_gemObtained)
        {
            remainingTime += (remainingTime > 0) ? targetTime - 1 : targetTime;
            player.DJ_gemObtained = false;
        }

        if (DJ_enabled)
        {
            textField.text = "Double jump\nRemaining time: " + (int)remainingTime;
            if (remainingTime > 0) remainingTime -= Time.deltaTime;
            else timerEnded();
        }
        else textField.text = "";
    }

    private void timerEnded() 
    {
        player.djEnabled = false;
        remainingTime = 0;
        DJ_enabled = false;
        Debug.Log("Double Jump Enabled: " + player.djEnabled);
    }
}
