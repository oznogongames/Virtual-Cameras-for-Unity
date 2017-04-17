using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    public float scrollZone = 30.0f;    //Detection zone variable
    public float scrollSpeed = 25.0f;    //Scroll speed variable

    public float xMax = 0.0f;           //Maximum x value of the camera
    public float xMin = -60.0f;           //Minimum x value of the camera
    public float yMax = 21.0f;          //Maximum y value of the camera
    public float yMin = 5.0f;           //Minimum y value of the camera
    public float zMax = 60.0f;           //Maximum z value of the camera
    public float zMin = 0.0f;           //Minimum z value of the camera

    private Vector3 desiredPosition;    //3D vector for the desired position of the camera

    //Start method is called at initiation
    private void Start()
    {
        desiredPosition = transform.position; //Store initial position into desired position for reference
    }

    //Update is called once per frame
    private void Update()
    {
        float x = 0, y = 0, z = 0;  //3D vector variables
        float speed = scrollSpeed * Time.fixedDeltaTime;  //Speed variable

        //Mouse input
        if (Input.mousePosition.x < scrollZone)
        {
            x -= speed;
        }
        else if (Input.mousePosition.x > Screen.width - scrollZone)
        {
            x += speed;
        }

        if (Input.mousePosition.y < scrollZone || Input.GetKey("s"))
        {
            z -= speed;
        }
        else if (Input.mousePosition.y > Screen.height - scrollZone)
        {
            z += speed;
        }

        //Keyboard input
        if (Input.GetKey("a") && transform.position.x > xMin)
        {
            x -= speed;
        }
        else if (Input.GetKey("d") && transform.position.x < xMax)
        {
            x += speed;
        }

        if (Input.GetKey("s") && transform.position.z > zMin)
        {
            z -= speed;
        }
        else if (Input.GetKey("w") && transform.position.z < zMax)
        {
            z += speed;
        }

        y += Input.GetAxis("Mouse ScrollWheel") * speed * 5.0f; //Scrollwheel input of the mouse for Y movement

        Vector3 move = new Vector3(x, -y, z) + desiredPosition; //Fill 3D vector with input
        move.x = Mathf.Clamp(move.x, xMin, xMax);   //Update X-axis value within limit
        move.y = Mathf.Clamp(move.y, yMin, yMax);   //Update Y-axis value within limit
        move.z = Mathf.Clamp(move.z, zMin, zMax);   //Update Z-axis value within limit
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);   //Interpolate between current and desired position and assign the result
    }
}