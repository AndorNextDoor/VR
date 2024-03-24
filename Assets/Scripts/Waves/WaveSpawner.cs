using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    [Header("Wave Settings")]
    [SerializeField] private float timeBetweenWaves;
    private float waveTimer;

    [SerializeField] private WaveDefinition[] waves;
    //[SerializeField] private Wave[] waves;
    private int waveIndex;

    [HideInInspector]
    public bool needToSpawnWave;

    private int enemiesSpawnedAmount;

    [SerializeField] private Transform[] spawnPos;

    public float timeSpeed = 1;

    private void Awake()
    {
        instance = this;
        needToSpawnWave = true;
        waveTimer = timeBetweenWaves;
    }

    private void Update()
    {
        if (!needToSpawnWave) return;

        waveTimer -= Time.deltaTime;

        UiManager.instance.SetWaveTimer((int)waveTimer);


        if (waveTimer <= 0 && needToSpawnWave)
        {
            needToSpawnWave = false;
            enemiesSpawnedAmount = 0;
            waveTimer = timeBetweenWaves;

            if (waveIndex > waves.Length - 1)
            {
                GameManager.Instance.OnLevelCompletion();
            }
            else
            {
                needToSpawnWave = false;
                StartCoroutine(Spawn());
            }
        }

        
    }

    IEnumerator Spawn()
    {
        enemiesSpawnedAmount = waves[waveIndex].enemies.Length;
        GameManager.Instance.SetEnemiesAmount(enemiesSpawnedAmount);

        foreach (WaveDefinition.EnemyInfo enemy in waves[waveIndex].enemies)
        {

            int randomSpawnPos = Random.Range(0, spawnPos.Length);

            Vector3 _spawnPos = spawnPos[randomSpawnPos].transform.position;
            _spawnPos.y = enemy.enemyPrefab.transform.position.y;

            GameObject spawnedEnemy = Instantiate(enemy.enemyPrefab, _spawnPos, enemy.enemyPrefab.transform.rotation);
          
            yield return new WaitForSeconds(enemy.spawnDelay);
        }


        yield return null;
        waveIndex++;
    }

}
