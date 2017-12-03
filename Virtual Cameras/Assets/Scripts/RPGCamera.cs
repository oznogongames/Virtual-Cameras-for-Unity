using UnityEngine;

public class RPGCamera : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;
    [Header("Camera settings")]
    [Tooltip("Current relative offset to the target")]
    public Vector3 offset;
    [Tooltip("Minimum relative offset to the target GameObject")]
    public Vector3 minOffset;
    [Tooltip("Maximum relative offset to the target GameObject")]
    public Vector3 maxOffset;
    [Tooltip("Rotation limits for the X-axis in degrees")]
    public Vector2 rotationLimitsX;
    [Tooltip("Rotation limits for the Y-axis in degrees")]
    public Vector2 rotationLimitsY;
    [Tooltip("Whether the rotation on the X-axis should be limited")]
    public bool limitXRotation;
    [Tooltip("Whether the rotation on the Y-axis should be limited")]
    public bool limitYRotation;
    [Header("Mouse settings")]
    [Tooltip("Rotation speed for the X and Y-axis")]
    public Vector2 rotationSpeed;
    [Tooltip("Scroll wheel multiplier to change the offset")]
    public float scrollSpeed;
    [Tooltip("Whether the cursor should be hidden in playmode")]
    public bool hideCursor;
    [Tooltip("Whether the cursor should be locked in playmode")]
    public bool lockCursor;

    private Transform _transform;
    private Vector2 _rotation;

    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("No target found!");
        }

        if (hideCursor)
        {
            Cursor.visible = false;
        }
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        _transform = transform;
        SetupCamera();
    }

    // Update is called once per frame
    void Update()
    {   
        if (target && Input.GetMouseButton(1))
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

        if (-offset.z > -minOffset.z)
        {
            offset.z = minOffset.z;
        }
        else if (-offset.z < -maxOffset.z)
        {
            offset.z = maxOffset.z;
        }
        else
        {
            offset.z -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
        }

        Quaternion rotation = Quaternion.Euler(_rotation.y, _rotation.x, 0);
        Vector3 position = rotation * new Vector3(offset.x, offset.y, -offset.z) + target.position;

        _transform.rotation = rotation;
        _transform.position = position;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SetupCamera()
    {
        _transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z - offset.z);
        _transform.LookAt(target);
    }

    public void ResetCamera()
    {
        _transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.x, target.position.z - offset.z);
        _transform.LookAt(target);
        _rotation.x = 0;
        _rotation.y = 0;
    }
}
