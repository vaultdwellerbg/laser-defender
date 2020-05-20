using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWaveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfigs[startingWaveIndex];
        StartCoroutine(SpawnEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnEnemiesInWave(WaveConfig wave)
    {
        var numberOfEnemies = wave.GetNumberOfEnemies();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(wave.GetEnemyPrefab(), wave.GetWaypoints()[0].transform.position, Quaternion.identity);

            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }
}
