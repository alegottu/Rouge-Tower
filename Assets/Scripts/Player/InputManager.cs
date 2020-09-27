using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public float movement = 0;
    [HideInInspector] public Vector3 mousePos = Vector3.zero;
    [HideInInspector] public bool attack = false, utility = false;

    [SerializeField] private float smoothRate = 1;

    private Camera cam = CameraManager.currentCamera;

    private void Update()
    {
        movement = Mathf.SmoothStep(movement, Input.GetAxisRaw("Horizontal"), smoothRate);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        attack = Input.GetMouseButton(0);
        utility = Input.GetKey(KeyCode.Q);
    }
}
