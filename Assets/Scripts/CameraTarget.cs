using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<VirtualCameraController>().AssignTarget(gameObject.transform);
    }
}
