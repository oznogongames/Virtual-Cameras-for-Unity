using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("Factor for camera movement upwards")]
    public float climbSpeed = 4;
    [Tooltip("Factor for normal camera movement")]
    public float normalMoveSpeed = 10;
    [Tooltip("Factor for slower camera movement")]
    public float slowMoveFactor = 0.25f;
    [Tooltip("Factor for faster camera movement")]
    public float fastMoveFactor = 3;
    [Tooltip("Rotation limits for the X-axis in degrees")]
    public Vector2 rotationLimitsX;
    [Tooltip("Rotation limits for the X-axis in degrees")]
    public Vector2 rotationLimitsY;
    [Tooltip("Whether the rotation on the X-axis should be limited")]
    public bool limitXRotation;
    [Tooltip("Whether the rotation on the Y-axis should be limited")]
    public bool limitYRotation;
    [Header("Keyboard settings")]
    [Tooltip("Key for moving the camera upwards")]
    public KeyCode moveUp;
    [Tooltip("Key for moving the camera downwards")]
    public KeyCode moveDown;
    [Tooltip("Key for faster camera movement")]
    public KeyCode moveFast;
    [Tooltip("Key for slower camera movement")]
    public KeyCode moveSlow;
    [Header("Mouse settings")]
    [Tooltip("Factor for camera sensitivity")]
    public float cameraSensitivity = 90;
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
        _rotation.x += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        _rotation.y += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        if (limitXRotation)
        {
            _rotation.x = Mathf.Clamp(_rotation.x, rotationLimitsX.x, rotationLimitsX.y);
        }
        if (limitYRotation)
        {
            _rotation.y = Mathf.Clamp(_rotation.y, rotationLimitsY.x, rotationLimitsY.y);
        }

        transform.localRotation = Quaternion.AngleAxis(_rotation.x, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(_rotation.y, Vector3.left);

        if (Input.GetKey(moveFast))
        {
            transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else if (Input.GetKey(moveSlow))
        {
            transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }

        if (Input.GetKey(moveUp))
        {
            transform.position += transform.up * climbSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveDown))
        {
            transform.position -= transform.up * climbSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
