using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected PlayerStats stats = null;
    [SerializeField] protected InputManager input = null;
    [SerializeField] protected Transform rotator;

    protected float attackTimer = 0;

    protected virtual void Update()
    {
        if (input.attack)
        {
            Attack();
        }

        attackTimer -= Time.deltaTime;
    }

    protected virtual void FixedUpdate()
    {
        transform.position += Vector3.right * input.movement * stats.speed * Time.deltaTime;

        Vector2 direction = (input.mousePos - rotator.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.eulerAngles = Vector3.forward * angle;

        transform.eulerAngles = angle > 90 ? Vector3.forward * 180 : Vector3.zero;
    }

    protected abstract void Attack();
}
