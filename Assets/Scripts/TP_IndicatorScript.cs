using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_IndicatorScript : MonoBehaviour
{
    public GameObject[] ammo;
    private PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        fillAmmo(player.remainingTeleports);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && player.remainingTeleports > 0) fillAmmo(player.remainingTeleports);

        if (player.TP_gemObtained)
        {
            fillAmmo(player.remainingTeleports);
            player.TP_gemObtained = false;
        }
    }

    public void fillAmmo(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            ammo[i].SetActive(true);
        }
        for (int i = amount; i < player.maxTeleports; i++)
            ammo[i].SetActive(false);
    }
}
