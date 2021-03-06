﻿using UnityEngine;

public class Gunner : Player
{
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GameObject bullet = null;

    protected override void Attack()
    {
        if (attackTimer <= 0)
        {
            GameObject bullet = Instantiate(this.bullet, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<Bullet>().SetAngle(rotator.eulerAngles.z);

            attackTimer = stats.attackSpeed;
        }
    }
}
