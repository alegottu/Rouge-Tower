using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event Action OnSpawningEnd;
    [HideInInspector] public float difficultyMultiplier = 1;

    [SerializeField] private EnemyArray[] enemySets = null;
    [SerializeField] private int[] setsPerWave = null;

    private GameObject currentEnemyObj = null;
    private Enemy currentEnemy = null;
    private Stage currentStage = null;
    private bool waveStarted = false;

    private void OnEnable()
    {
        currentStage = StageManager.currentStage;

        EnemyManager.currentSpawners.Add(this);
        currentStage.OnWaveChange += WaveStartEventHandler;
    }

    private IEnumerator SpawnCoroutine()
    {
        for (int wave = 1; wave <= currentStage.maxWaves; wave++)
        {
            for (int set = 0; set < setsPerWave[wave - 1]; set++)
            {
                EnemyArray currentEnemySet = enemySets[set];
                GameObject[] enemiesToSpawn = currentEnemySet.enemies;

                for (int enemy = 0; enemy < enemiesToSpawn.Length; enemy++)
                {
                    currentEnemyObj = Instantiate(enemiesToSpawn[enemy], transform.position, Quaternion.identity);
                    currentEnemy = currentEnemyObj.GetComponent<Enemy>();
                    currentEnemy.SetDifficulty(difficultyMultiplier);
                    yield return new WaitForSeconds(currentEnemySet.timePerEnemy[enemy]);
                }

                currentEnemy.OnTriggerDeath += TriggerDeathEventHandler;
                yield return new WaitUntil(() => !currentEnemy);
            }

            OnSpawningEnd?.Invoke();
            waveStarted = false;
            yield return new WaitUntil(() => waveStarted);
        }
    }

    private void WaveStartEventHandler(float difficulty)
    {
        difficultyMultiplier = difficulty;
        waveStarted = true;

        StartCoroutine(SpawnCoroutine());
    }

    private void TriggerDeathEventHandler()
    {
        currentEnemy.OnTriggerDeath -= TriggerDeathEventHandler;
        currentEnemy = null;
    }

    private void OnDestroy()
    {
        EnemyManager.currentSpawners.Remove(this);
    }
}
