using UnityEngine;

public class MTPCamera : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("Current relative offset to the target")]
    public Vector3 offset;
    [Tooltip("Rotation limits for the X-axis in degrees")]
    public Vector2 rotationLimitsX;
    [Tooltip("Rotation limits for the X-axis in degrees")]
    public Vector2 rotationLimitsY;
    [Tooltip("Whether the rotation on the X-axis should be limited")]
    public bool limitXRotation;
    [Tooltip("Whether the rotation on the Y-axis should be limited")]
    public bool limitYRotation;
    [Header("Mouse settings")]
    [Tooltip("Rotation speed for the X and Y-axis")]
    public Vector2 rotationSpeed;
    [Tooltip("Whether the cursor should be hidden in playmode")]
    public bool hideCursor;
    [Tooltip("Whether the cursor should be locked in playmode")]
    public bool lockCursor;

    private Vector2 _rotation;

    // Use this for initialization
    void Start()
    {
        if (hideCursor)
        {
            Cursor.visible = false;
        }
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                gameObject.transform.position = hit.transform.position + new Vector3(offset.x, offset.y, offset.z);
            }
        }

        if (Input.GetMouseButton(1))
        {
            _rotation.x += Input.GetAxis("Mouse X") * rotationSpeed.x * Time.deltaTime;
            _rotation.y -= Input.GetAxis("Mouse Y") * rotationSpeed.y * Time.deltaTime;

            if (limitXRotation)
            {
                _rotation.x = Mathf.Clamp(_rotation.x, rotationLimitsX.x, rotationLimitsX.y);
            }
            if (limitYRotation)
            {
                _rotation.y = Mathf.Clamp(_rotation.y, rotationLimitsY.x, rotationLimitsY.y);
            }
        }

        Quaternion rotation = Quaternion.Euler(_rotation.y, _rotation.x, 0);
        transform.rotation = rotation;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
