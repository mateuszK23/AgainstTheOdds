using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private Camera theCam;
    public GameObject[] ammo;
    public GameObject bullet;
    public Transform firePoint;
    private PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        theCam = Camera.main;
        fillAmmo(player.remainingFireBalls);
        for (int i = player.remainingFireBalls; i < player.maxAmmo; i++) ammo[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //mouseRead();
        fireBullet();
        if (player.FB_gemObtained)
        {
            fillAmmo(player.remainingFireBalls);
            player.FB_gemObtained = false;
        }
    }

    private void fireBullet()
    {
        if (Input.GetMouseButtonDown(0) && player.remainingFireBalls > 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            player.remainingFireBalls--;
            ammo[player.remainingFireBalls].SetActive(false);
        }
    }

    public void fillAmmo(int amount)
    {
        for(int i=0; i<amount; i++)
        {
            ammo[i].SetActive(true);
        }
    }
}
