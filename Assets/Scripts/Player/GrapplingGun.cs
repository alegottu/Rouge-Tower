using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField] private Transform userTransform = null;
    [SerializeField] private Rigidbody2D userRb = null;
    [SerializeField] private InputManager input = null;
    [SerializeField] private LineRenderer grappleLine = null;
    [SerializeField] private LayerMask grappleableLayers = new LayerMask();
    [SerializeField] private float maxDistance = 1, speed = 1, pullForce = 1, cooldown = 1;

    private RaycastHit2D grappleHit = new RaycastHit2D();
    private Vector3 direction = Vector3.zero;
    private bool allowGrapple = true;
    private float cooldownTimer = 0;

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        allowGrapple = cooldownTimer <= 0 ? true : allowGrapple;
    }

    private void FixedUpdate()
    {
        grappleHit = Physics2D.Raycast(transform.position, transform.right, maxDistance, grappleableLayers);

        if (input.utility && grappleHit && allowGrapple)
        {
            StartGrapple();
        }
    }

    private void StartGrapple()
    {
        cooldownTimer = cooldown;

        direction = ((Vector3)grappleHit.point - transform.position).normalized;
        userTransform.position += direction * speed * Time.deltaTime;
        DrawGrapple();

        if (!input.utility)
        {
            StopGrapple();
        }
    }

    private void StopGrapple()
    {
        userRb.AddForce(direction * pullForce);
        allowGrapple = false;

        grappleLine.enabled = false;
    }

    private void DrawGrapple()
    {
        grappleLine.enabled = true;

        grappleLine.SetPositions(new Vector3[] { transform.position, grappleHit.point });
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.Equals(grappleHit.collider) && input.utility)
        {
            StopGrapple();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * maxDistance);
        Gizmos.color = Color.white;
    }
}