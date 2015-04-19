using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 spawnValues;
    public int enemyCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int maxEnemy;


    //Food
    public GameObject food;
    public Vector3 foodSpawnValues;
    public int foodCount;
    public float foodSpawnWait;
    public float foodStartWait;
    public float foodWaveWait;
    public int maxFood;

    //Alcohol
    public GameObject alcohol;
    public Vector3 alcSpawnValues;
    public int alcCount;
    public float alcSpawnWait;
    public float alcStartWait;
    public float alcWaveWait;
    public int maxAlc;

    public GameObject player;
    public int spawnNum;


    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(startWait);
            spawnNum = (int)Random.Range(0, 100);
            Debug.Log("SpanNum: " + spawnNum);
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
            Vector3 spawnPosition = new Vector3(playerpos.x + Random.Range(10,40), playerpos.y + 10, playerpos.z);
            Debug.Log("SpawnEnemy: " + spawnPosition + " enemycount: " + enemyCount);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemy, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    IEnumerator SpawnFood()
    {
        if (foodCount < maxFood)
        {
            Vector3 playerpos = player.transform.position;
            Vector3 spawnPosition = new Vector3(playerpos.x + Random.Range(10, 40), playerpos.y + 10, playerpos.z);
            Debug.Log("Food Spawn: " + spawnPosition + " Foodcount: " + foodCount);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(food, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(foodSpawnWait);
        }
    }

    IEnumerator SpawnAlc()
    {
        if (alcCount < maxAlc)
        {
            Vector3 playerpos = player.transform.position;
            Vector3 spawnPosition = new Vector3(playerpos.x + Random.Range(10, 40), playerpos.y + 10, playerpos.z);
            Debug.Log("Alc Spawn: " + spawnPosition + " Alccount: " + alcCount);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(alcohol, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(alcSpawnWait);
        }
    }
}