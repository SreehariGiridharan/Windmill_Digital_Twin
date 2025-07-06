using UnityEngine;

public class SceneViewCameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float zoomSpeed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float panSpeed = 0.5f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        if (target == null)
        {
            GameObject dummyTarget = new GameObject("Camera Target");
            dummyTarget.transform.position = transform.position + transform.forward * distance;
            target = dummyTarget.transform;
        }
    }

    void LateUpdate()
    {
        // Rotate (Left Mouse)
        if (Input.GetMouseButton(0))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -89, 89);
        }

        // Zoom (Scroll Wheel)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, 2f, 50f); // prevent too close/far

        // Pan (Right Mouse)
        if (Input.GetMouseButton(1))
        {
            Vector3 pan = new Vector3(-Input.GetAxis("Mouse X") * panSpeed, -Input.GetAxis("Mouse Y") * panSpeed, 0);
            target.transform.Translate(pan);
        }

        // Apply rotation and position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
