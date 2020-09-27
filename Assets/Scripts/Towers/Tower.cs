using UnityEngine;
using System.Collections;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected GameObject bullet = null;
    [SerializeField] protected Transform shootPoint = null;
    [SerializeField] protected float shootCooldown = 0;

    protected bool activated = false;
    protected Enemy currentTarget = null;

    protected abstract void Shoot();
    protected virtual IEnumerator ShootCoroutine()
    {
        while (activated)
        {
            Shoot();
            yield return new WaitForSeconds(shootCooldown);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Enemy enemy))
        {
            currentTarget = enemy;
        }
    }

    protected virtual void Update()
    {
        activated = currentTarget;
    }
}
