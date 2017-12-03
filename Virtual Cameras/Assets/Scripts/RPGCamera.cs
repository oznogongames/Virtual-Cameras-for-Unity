using UnityEngine;

public class RPGCamera : MonoBehaviour
{
    public Transform target;
    public float walkDistance;
    public float runDistance;
    public float height;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    private Transform _myTransform;
    private float x;
    private float y;
    private bool camButtonDown = false;

    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("No target found!");
        }

        _myTransform = transform;
        SetupCamera();
    }

    // Update is called once per frame
    void Update()
    {   
        if (target && Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            x = Mathf.Clamp(x, -90.0f, 90.0f);
            y = Mathf.Clamp(y, -90.0f, 90.0f);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -walkDistance) + target.position;

            _myTransform.rotation = rotation;
            _myTransform.position = position;

        }
    }

    public void SetupCamera()
    {
        _myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
        _myTransform.LookAt(target);
    }

    public void ResetCamera()
    {
        _myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
        _myTransform.LookAt(target);
        x = 0;
        y = 0;
    }
}
