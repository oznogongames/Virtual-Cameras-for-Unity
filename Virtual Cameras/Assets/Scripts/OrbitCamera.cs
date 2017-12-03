using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;
    [Tooltip("Current relative offset to the target")]
    public Vector3 offset;
    [Tooltip("Rotation speed for the X-axis")]
    public float rotationSpeed = 20f;
    [Tooltip("Whether the camera should rotate left")]
    public bool rotateLeft = false;
    [Tooltip("Whether the cursor should be hidden in playmode")]
    public bool hideCursor = false;
    [Tooltip("Whether the cursor should be locked in playmode")]
    public bool lockCursor = false;

    private Transform _transform;
    private Vector2 _rotation;

    // Use this for initialization
    void Start ()
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

        SetupCamera();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (rotateLeft)
        {
            transform.RotateAround(target.position, new Vector3(0.0f, 1.0f, 0.0f), rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(target.position, new Vector3(0.0f, -1.0f, 0.0f), rotationSpeed * Time.deltaTime);
        }

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
        _transform.rotation = Quaternion.Euler(_rotation.y, _rotation.x, 0);
    }
}
