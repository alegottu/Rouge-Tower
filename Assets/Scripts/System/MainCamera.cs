using UnityEngine;
using Cinemachine;

public class MainCamera : MonoBehaviour
{
    private Camera cam = null;
    private CinemachineVirtualCamera vCam = null;

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
        vCam = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        CameraManager.currentCamera = cam;
        CameraManager.activeCameras.Add(cam);

        PlayerManager.OnPlayerActive += SetTarget;
    }

    private void OnDisable()
    {
        CameraManager.currentCamera = null;
        CameraManager.activeCameras.Remove(cam);

        PlayerManager.OnPlayerActive -= SetTarget;
    }

    private void SetTarget(Player player)
    {
        vCam.Follow = player.transform;
    }
}
