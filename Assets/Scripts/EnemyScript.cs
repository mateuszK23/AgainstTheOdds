using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private PlayerScript player;
    public GameObject headTop;
    public float movementSpeed = 3;
    public bool goingLeft = true;
    public GameObject dieEffect;
    public GameObject[] gems;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(player) ReadHeadTop();
        Movement();
    }

    private void Movement()
    {
        int movementX = goingLeft ? -1 : 1;
        float x = transform.localScale.x;
        float tempX;

        if (goingLeft) tempX = (x > 0) ? x : -x;
        else tempX = (x > 0) ? -x : x;

        transform.localScale = new Vector3(tempX, transform.localScale.y, transform.localScale.z);
        transform.position += new Vector3(movementX, 0f, 0f) * movementSpeed * Time.deltaTime;
    }

    private void ReadHeadTop()
    {
        if(player.transform.position.y > headTop.transform.position.y)
        {     
            headTop.SetActive(true);
        }

        if(player.grounded) headTop.SetActive(false);
    }
    public void Die(bool killedByPlayer)
    {
        GameObject ef = Instantiate(dieEffect, transform.position, Quaternion.identity);
        ParticleSystem px = ef.GetComponent<ParticleSystem>();

        ParticleSystem.ShapeModule editableShape = px.shape;
        editableShape.position = new Vector3(0f, -0.8f, 0f);

        px.Play();
        Destroy(ef, px.main.duration);
        if(killedByPlayer)
        {
            if(Random.Range(0,10) > (3 + (int)4*StateController.difficulty))
            {
                GameObject gem = Instantiate(gems[Random.Range(0, gems.Length)]);
                gem.transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("WorldBoundary"))
        {
            Die(false);
        }
    }
}
