using UnityEngine;

public class RPGCamera : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;
    [Tooltip("Relative offset from the target position")]
    public Vector3 offset;
    [Tooltip("Maximum relative offset from the target position")]
    public Vector3 maxOffset;
    [Header("Camera")]
    [Tooltip("Rotation speed")]
    public Vector2 rotationSpeed;
    [Tooltip("Rotation limits for the Y-axis")]
    public Vector2 rotationLimits;
    [Tooltip("Speed scalar for the mouse wheel")]
    public float scrollSpeed = 25.0f;
    [Tooltip("Whether the player can change the rotation on the Y-axis of the camera")]
    public bool canChangeYAxis = true;

    private Vector3 position;

    //Use this for initialization
    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("No target found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target && Input.GetMouseButton(1))
        {
            position.x += Input.GetAxis("Mouse X") * rotationSpeed.x * Time.deltaTime;
            if (canChangeYAxis)
            {
                position.y -= Input.GetAxis("Mouse Y") * rotationSpeed.y * Time.deltaTime;
            }
            position.y = Mathf.Clamp(position.y, rotationLimits.x, rotationLimits.y);
        }

        if (transform.position.z > target.transform.position.z - offset.z)
        {
            position.z = target.transform.position.z + offset.z;
        }
        else if (transform.position.z < target.transform.position.z - maxOffset.z)
        {
            position.z = target.transform.position.z + maxOffset.z;
        }
        else
        {
            position.z -= Input.GetAxis("Mouse ScrollWheel") * Time.fixedDeltaTime * scrollSpeed;
        }

        transform.rotation = Quaternion.Euler(position.y, position.x, 0.0f);
        transform.position = transform.rotation * new Vector3(offset.x, offset.y, -position.z) + target.position;
    }
}
