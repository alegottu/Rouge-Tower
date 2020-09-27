using UnityEngine;

public class Sentry : Tower
{
    protected override void Shoot()
    {
        GameObject bullet = Instantiate(this.bullet, shootPoint.position, Quaternion.identity);

        Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        bullet.GetComponent<Bullet>().SetAngle(angle);
    }
}
