using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform destination = null;

    private Player player = null;

    private void Update()
    {
        if (player && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = destination.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            this.player = player;
        }
    }

    private void OnTriggerExit2D()
    {
        player = null;
    }
}
