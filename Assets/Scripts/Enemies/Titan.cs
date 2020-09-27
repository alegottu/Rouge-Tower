using UnityEngine;

public class Titan : Enemy
{
    protected override void Move()
    {
        base.Move();

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, stats.speed);
    }

    protected override void Wander()
    {
        transform.position += Vector3.right * stats.speed;
    }

    protected override void Attack()
    {
        player.TakeDamage(Mathf.RoundToInt(stats.damage * difficultyMultiplier));
        attackTimer = stats.attackSpeed;
    }
}
