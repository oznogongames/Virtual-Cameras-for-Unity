using UnityEngine;

public class MTPCamera : MonoBehaviour
{
    public float yOffset = 1.0f;
    public float xSpeed = 250.0f;		//Rotation speed on the x-axis
    public float ySpeed = 120.0f;       //Rotation speed on the y-axis
    public float zSpeed = 120.0f;       //Rotation speed on the z-axis
    public float yMinLimit = -90.0f;    //Least possible value of the y rotation
    public float yMaxLimit = 90.0f;		//Maximum possible value of the y rotation
    private float x;					//X position value
    private float y;                    //Y position value

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Check for left mouse button press
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                gameObject.transform.position = hit.transform.position + new Vector3(0.0f, yOffset, 0.0f);
            }
        }

        if (Input.GetMouseButton(1)) //Check for right mouse button press 
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f; //Update X-axis based on mouse input
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f; //Update Y-axis based on mouse input
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);       //Update the current Y-axis value by interpolating it between the limit
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);   //Rotation euler angles

        transform.rotation = rotation;  //Assign the new rotation/orientation
    }
}
