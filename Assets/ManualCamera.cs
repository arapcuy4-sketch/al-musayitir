using UnityEngine;

public class ManualCamera : MonoBehaviour
{
    public Transform target;

    [Header("Position Settings")]
    public float distance = 3f;
    public float height = 1.7f;
    public float sensitivity = 170f;
    public float smoothSpeed = 10f;

    private float currentX;
    private float currentY = 20f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        currentX = angles.y;
        currentY = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ===== ROTATE WITH RIGHT CLICK =====
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            currentX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            currentY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            currentY = Mathf.Clamp(currentY, -10f, 70f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // ===== ZOOM =====
        distance -= Input.GetAxis("Mouse ScrollWheel") * 5f;
        distance = Mathf.Clamp(distance, 1.5f, 8f);

        // ===== CALCULATE ROTATION =====
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // ===== CALCULATE POSITION =====
        Vector3 desiredPosition = target.position 
                                  + Vector3.up * height 
                                  - (rotation * Vector3.forward * distance);

        // ===== SMOOTH MOVE =====
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }
}