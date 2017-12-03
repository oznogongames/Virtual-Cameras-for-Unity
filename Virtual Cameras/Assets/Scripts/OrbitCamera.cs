using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;
    [Tooltip("Current relative offset to the target")]
    public Vector3 offset;
    [Tooltip("Rotation speed for the X-axis")]
    public float rotationSpeed;
    [Tooltip("Whether the camera should rotate left")]
    public bool rotateLeft;

    private Transform _transform;
    private Vector2 _rotation;

    // Use this for initialization
    void Start ()
    {
        if (target == null)
        {
            Debug.LogWarning("No target found!");
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
