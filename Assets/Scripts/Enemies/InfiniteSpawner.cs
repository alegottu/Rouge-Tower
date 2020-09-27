using UnityEngine;
using System.Collections;

public class InfiniteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy = null;
    [SerializeField] private float timePerSpawn = 0;

    private Stage currentStage = null;
    private float difficultyMultiplier = 1;
    private bool canSpawn = false;

    private void OnEnable()
    {
        currentStage = StageManager.currentStage;
        currentStage.OnWaveChange += WaveStartEventHandler;

        EnemyManager.OnWaveEnd += WaveEndEventHandler;
    }

    private IEnumerator SpawnCoroutine()
    {
        while (canSpawn)
        {
            GameObject currentEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            currentEnemy.GetComponent<Enemy>().SetDifficulty(difficultyMultiplier);
            yield return new WaitForSeconds(timePerSpawn);
        }
    }

    private void WaveStartEventHandler(float difficulty)
    {
        canSpawn = true;
        difficultyMultiplier = difficulty;
    }

    private void WaveEndEventHandler()
    {
        canSpawn = false;
    }
}
