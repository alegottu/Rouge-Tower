using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static Action OnWaveEnd;
    public static List<Enemy> currentEnemies = new List<Enemy>();
    public static List<EnemySpawner> currentSpawners = new List<EnemySpawner>();

    private int currentSpawnerAmount = 0;
    private int spawnersFinished = 0;

    private void Update()
    {
        if (currentSpawners.Count > currentSpawnerAmount)
        {
            currentSpawners[currentSpawners.Count - 1].OnSpawningEnd += OnSpawningEndEventHandler;
            currentSpawnerAmount++;
        }
    }

    private void OnSpawningEndEventHandler()
    {
        spawnersFinished++;

        if (spawnersFinished >= currentSpawnerAmount)
        {
            OnWaveEnd?.Invoke();
            spawnersFinished = 0;
        }
    }
}
