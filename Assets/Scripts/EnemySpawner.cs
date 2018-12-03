using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<WaveConfig> waveConfigs;

    [SerializeField]
    private int startingWave = 0;

    [SerializeField]
    private bool looping = false;


    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while(looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            WaveConfig currentWave = waveConfigs[waveIndex];

            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int enemyIndex = 0; enemyIndex < waveConfig.GetNumberOfEnemies(); enemyIndex++)
        {
            GameObject enemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].position, Quaternion.identity);

            enemy.GetComponent<EnemyPathfinding>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}