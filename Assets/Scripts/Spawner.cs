using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyScript[] enemies;
    public Transform[] spawners;
    private EnemyScript enemy;
  

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        Debug.Log("Difficulty: " + StateController.difficulty);
    }


    IEnumerator SpawnEnemies()
    {
        for(;;)
        {
            yield return new WaitForSeconds(Random.Range(1, 5 - (4 * StateController.difficulty)));
            int randomIndex = Random.Range(0, enemies.Length);
            int randomSpawnerIndex = Random.Range(0, spawners.Length);
            float randomScale = Random.Range(0.95f, 1.3f);

            enemy = Instantiate(enemies[randomIndex]);
            enemy.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            enemy.transform.position = spawners[randomSpawnerIndex].position;
            enemy.goingLeft = randomSpawnerIndex == 0 ? false : true;
        }
    }
}
