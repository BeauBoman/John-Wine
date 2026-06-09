using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private bool hasPhase;

    public static WaveSpawner instance;
    private void Start()
    {
        instance = this;
    }
    public void StartWave(float delay, int waveSize)
    {
        StartCoroutine(SpawnWave(delay, waveSize));
    }
    private IEnumerator SpawnWave(float delay, int waveSize)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < waveSize; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            int randomSpawn = Random.Range(0, spawnPoints.Length);

            GameObject newEnemy = Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawn], Quaternion.identity);

            if (hasPhase == true)
            {
                PhaseManager.instance.
            }
        }
    }
}
