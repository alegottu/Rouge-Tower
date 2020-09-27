using System;
using UnityEngine;

public class ClickSelect : MonoBehaviour
{
    public static event Action<GameObject, Vector3> OnSelectedObjectChange;
    private GameObject selectedObject = null;

    [SerializeField] private Camera cam = null;
    [SerializeField] private LayerMask layersToHit = new LayerMask();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D click = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layersToHit);

            if (click)
            {
                selectedObject = click.collider.gameObject;
                OnSelectedObjectChange?.Invoke(selectedObject, clickPoint);
            }
        }
    }
}
