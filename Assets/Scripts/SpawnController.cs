using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    //enemy
    public GameObject enemy;
    public Vector3 spawnValues;
    public int enemyCount = 0;
    public float spawnWait = 0.3F;
    public float startWait = 2;
    public float waveWait = 2;
    public int maxEnemy = 10;


    //Food
    public GameObject food;
    public Vector3 foodSpawnValues;
    public int foodCount = 0;
    public float foodSpawnWait = 0.3F;
    public float foodStartWait = 2;
    public float foodWaveWait = 2;
    public int maxFood = 5;

    //Alcohol
    public GameObject alcohol;
    public Vector3 alcSpawnValues;
    public int alcCount = 0;
    public float alcSpawnWait = 0.3F;
    public float alcStartWait = 2;
    public float alcWaveWait = 2;
    public int maxAlc = 2;

    public GameObject player;
    public int spawnNum;


    void Start()
    {
        //enemy
        enemyCount = 0;
        spawnWait = 0.3F;
        startWait = 2;
        waveWait = 2;
        maxEnemy = 10;

        //food
        foodCount = 0;
        foodSpawnWait = 0.3F;
        foodStartWait = 2;
        foodWaveWait = 2;
        maxFood = 5;

        //alcohol
        alcCount = 0;
        alcSpawnWait = 0.3F;
        alcStartWait = 2;
        alcWaveWait = 2;
        maxAlc = 2;

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(startWait);
            spawnNum = (int)Random.Range(0, 100);
            //Debug.Log("SpanNum: " + spawnNum);
            if (spawnNum < 10)
            {
                StartCoroutine(SpawnAlc());
            }
            else if (spawnNum >= 10 && spawnNum < 20)
            {
                StartCoroutine(SpawnFood());
            }
            else
            {
                StartCoroutine(SpawnEnemy());
            }

        }

    }

    IEnumerator SpawnEnemy()
    {
        if (enemyCount < maxEnemy)
        {
            Vector3 playerpos = player.transform.position;
            Vector3 spawnPosition = new Vector3(playerpos.x + Random.Range(10, 40), 1.5f, 0.0f);
            //Debug.Log("SpawnEnemy: " + spawnPosition + " enemycount: " + enemyCount);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemy, spawnPosition, spawnRotation);
            enemyCount++;
            yield return new WaitForSeconds(spawnWait);
        }
    }

    IEnumerator SpawnFood()
    {
        if (foodCount < maxFood)
        {
            Vector3 playerpos = player.transform.position;
            Vector3 spawnPosition = new Vector3(playerpos.x + Random.Range(10, 40), 1.5f, -0.2f);
            //Debug.Log("Food Spawn: " + spawnPosition + " Foodcount: " + foodCount);
            Quaternion spawnRotation = Quaternion.identity;
            Transform new_food = Instantiate(food, spawnPosition, spawnRotation) as Transform;
            //Destroy(new_food.gameObject, 10.0f);
            yield return new WaitForSeconds(foodSpawnWait);
        }
    }

    IEnumerator SpawnAlc()
    {
        if (alcCount < maxAlc)
        {
            Vector3 playerpos = player.transform.position;
            Vector3 spawnPosition = new Vector3(playerpos.x + Random.Range(10, 40), 1.5f, -0.2f);
            //Debug.Log("Alc Spawn: " + spawnPosition + " Alccount: " + alcCount);
            Quaternion spawnRotation = Quaternion.identity;
            Transform new_alc = Instantiate(alcohol, spawnPosition, spawnRotation) as Transform;
            //Destroy(new_alc.gameObject, 10.0f);
            yield return new WaitForSeconds(alcSpawnWait);
        }
    }
}
