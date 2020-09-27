using System;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public event Action<float> OnWaveChange;
    public event Action<Item, Item, Item> OnStageReward;
    public float maxWaves = 0;

    [SerializeField] private float diffcultyScale = 1;
    [SerializeField] private ItemArray possibleItems = null;
    [SerializeField] private Transform waveRewardLocation = null;
    [SerializeField] private float timeUntilWave = 0;

    private int currentWave = 0;
    private float waveTimer = 0;
    private bool allowWave = true;

    private void OnEnable()
    {
        StageManager.currentStage = this;
        waveTimer = timeUntilWave;

        EnemyManager.OnWaveEnd += OnWaveEndEventHandler;
    }

    private void Update()
    {
        WaveCheck();
    }

    private void WaveCheck()
    {
        waveTimer -= Time.deltaTime;

        if (allowWave)
        {
            if (waveTimer != 0)
            {
                if (currentWave > 0)
                {
                    Item random = possibleItems.items[UnityEngine.Random.Range(0, possibleItems.items.Length)];
                    Instantiate(random, waveRewardLocation.position, Quaternion.identity);

                    waveTimer = timeUntilWave;
                }

                if (currentWave >= maxWaves)
                {
                    Item random = possibleItems.items[UnityEngine.Random.Range(0, possibleItems.items.Length)];
                    OnStageReward(random, random, random);

                    Destroy(gameObject);
                }
            }

            if (Input.GetKeyDown(KeyCode.E) || waveTimer <= 0)
            {
                OnWaveChange?.Invoke(diffcultyScale);

                currentWave++;
                waveTimer = 0;
                allowWave = false;
            }
        }
    }

    private void OnWaveEndEventHandler()
    {
        allowWave = true;
    }

    private void OnDestroy()
    {
        StageManager.currentStage = null;
        EnemyManager.OnWaveEnd -= OnWaveEndEventHandler;
    }
}
