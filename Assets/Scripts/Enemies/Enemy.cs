using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public event Action OnTriggerDeath;

    [SerializeField] protected EnemyStats stats = null;
    [SerializeField] private ItemArray possibleLoot = null;

    protected RaycastHit2D patrolHit = new RaycastHit2D();
    protected Health player = null;
    protected float difficultyMultiplier = 1;
    protected float attackTimer = 0;

    protected virtual void OnEnable()
    {
        EnemyManager.currentEnemies.Add(this);
    }

    public void SetDifficulty(float difficulty)
    {
        difficultyMultiplier = difficulty;
    }

    protected virtual void Update()
    {
        if (Physics2D.BoxCast(transform.position, stats.detectionRange, 0, Vector2.zero, stats.detectionRange.x, stats.targetLayer) && !player)
        {
            patrolHit = Physics2D.BoxCast(transform.position, stats.detectionRange, 0, Vector2.zero, stats.detectionRange.x, stats.targetLayer);
            player = patrolHit.collider.TryGetComponent(out Health health) ? health : player;
        }
    }

    protected virtual void Move()
    {
        if (!player)
        {
            Wander();
            return;
        }
    }

    protected abstract void Wander();

    protected abstract void Attack();

    //Default method; override completely if different case    
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (player)
        {
            if (collider.gameObject.Equals(player.gameObject) && attackTimer <= 0)
            {
                Attack();
            }
        }
    }

    protected virtual void OnDestroy()
    {
        if (UnityEngine.Random.Range(0, stats.lootChance) <= stats.lootChance)
        {
            Instantiate(possibleLoot.items[UnityEngine.Random.Range(0, possibleLoot.items.Length)]);
        }

        OnTriggerDeath?.Invoke();

        EnemyManager.currentEnemies.Remove(this);
    }
}
