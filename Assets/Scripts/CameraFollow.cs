using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private PlayerScript player;
    public float leftMaxX = -20.8f;
    public float rightMaxX = 20.8f;
    private Vector3 v;
    // Start is called before the first frame update
    void Start()
    {
        v.y = transform.position.y;
        v.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }

    private void followPlayer()
    {
        v.x = player.transform.position.x;
        if (v.x > leftMaxX && v.x < rightMaxX)
        {
            transform.position = v;
        }
        else
        {
            if (v.x > 0)
            {
                transform.position = new Vector3(rightMaxX, v.y, v.z);
            }
            else
            {
                transform.position = new Vector3(leftMaxX, v.y, v.z);
            }
        }
    }
}
