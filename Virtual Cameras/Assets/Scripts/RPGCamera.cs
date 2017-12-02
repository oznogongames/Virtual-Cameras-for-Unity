using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCamera : MonoBehaviour
{
    public Transform target; 		    //Target position and rotation
    public float offsetX = 0.0f;        //Offset on x-axis
    public float offsetY = 1.0f;        //Offset on y-axis
    public float offsetZ = 1.0f;
    public float maxOffsetZ = 5.0f;
    public float xSpeed = 250.0f;	    //Rotation speed on the x-axis
    public float ySpeed = 120.0f;       //Rotation speed on the y-axis
    public float zSpeed = 120.0f;       //Rotation speed on the z-axis
    public float yMinRotation = -90.0f;    //Least possible value of the y rotation
    public float yMaxRotation = 90.0f;		//Maximum possible value of the y rotation
    public float scrollSpeed = 25.0f;
    public bool canChangeY = true;
    private float x;				    //X position value
    private float y;				    //Y position value
    private float z = 3.0f;

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
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f; //Update X-axis based on mouse input
            if (canChangeY)
            {
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f; //Update Y-axis based on mouse input
            }
            y = Mathf.Clamp(y, yMinRotation, yMaxRotation);       //Update the current Y-axis value by interpolating it between the limit
        }

        if (transform.position.z > target.transform.position.z - offsetZ)
        {
            z = target.transform.position.z + offsetZ;
        }
        else if (transform.position.z < target.transform.position.z - maxOffsetZ)
        {
            z = target.transform.position.z + maxOffsetZ;
        }
        else
        {
            z -= Input.GetAxis("Mouse ScrollWheel") * Time.fixedDeltaTime * scrollSpeed;
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0.0f);  //Rotation euler angles
        Vector3 position = rotation * new Vector3(offsetX, offsetY, -z) + target.position; //Position is current rotation/orientation multiplied by the target input

        transform.rotation = rotation; //Assign new rotation
        transform.position = position; //Assign new position
    }
}
