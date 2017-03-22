using UnityEngine;

public class RPGCamera : MonoBehaviour 
{
	public Transform target; 		    //Target position and rotation
	public float zOffset;			    //Z-axis offset
    public float xSpeed = 250.0f;	    //Rotation speed on the x-axis
    public float ySpeed = 120.0f;	    //Rotation speed on the y-axis
	public float zSpeed = 120.0f;	    //Rotation speed on the z-axis
	public float yMinLimit = -90.0f;    //Least possible value of the y rotation
	public float yMaxLimit = 90.0f;		//Maximum possible value of the y rotation
    private float x;				    //X position value
    private float y;				    //Y position value

    //Use this for initialization
    void Start () 
	{
        if(target == null)  //Check for a target
		{
			Debug.LogWarning("VCSU ERROR: no target found!");
		}
    }

	//LateUpdate is called after all other updates are done
    void LateUpdate() 
	{    
    if (target && Input.GetMouseButton(1)) 
	{ 
		x += Input.GetAxis("Mouse X") * xSpeed * 0.02f; //Update X-axis based on mouse input
		y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f; //Update Y-axis based on mouse input
        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);       //Update the current Y-axis value by interpolating it between the limit
     }
     
     var rotation = Quaternion.Euler(y, x, 0);  //Rotation euler angles
     var position = rotation * new Vector3(0.0f, 0.0f, -zOffset) + target.position; //Position is current rotation/orientation multiplied by the target input
     
     transform.rotation = rotation; //Assign new rotation
     transform.position = position; //Assign new position
	}
}
