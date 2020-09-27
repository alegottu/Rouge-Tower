using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float fallForce = 0;
    [SerializeField] private float jumpForce = 0;
    [SerializeField] private float maxJumpVel = 0;

    private bool grounded = true;
    private bool doubleJump = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (grounded || doubleJump))
        {
            grounded = grounded? false: grounded;
            doubleJump = !grounded ? false : doubleJump;

            rb.velocity = Vector2.up * jumpForce;
        }

        if (rb.velocity.y <= maxJumpVel || !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallForce - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //possibly change to any collison
        grounded = !grounded ? collision.gameObject.TryGetComponent(out Ground ground) : grounded;
        doubleJump = grounded;
    }
}
