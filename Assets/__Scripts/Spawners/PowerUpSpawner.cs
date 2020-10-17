using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=WGn1zvLSndk
public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUps;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spwanMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;
    int randPowerUp;

    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    void Update()
    {
        spawnWait = Random.Range(spwanMostWait, spawnLeastWait);
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            // random powerups spawn
            randPowerUp = Random.Range(0, 2);
            Vector3 spawnposition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(powerUps[randPowerUp], spawnposition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }

}
