using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWaveIndex = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } 
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWaveIndex; i < waveConfigs.Count; i++)
        {
            yield return StartCoroutine(SpawnEnemiesInWave(waveConfigs[i]));
        }
    }

    private IEnumerator SpawnEnemiesInWave(WaveConfig wave)
    {
        var numberOfEnemies = wave.GetNumberOfEnemies();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var newEnemy = Instantiate(wave.GetEnemyPrefab(), wave.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);

            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }
}
