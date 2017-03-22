using UnityEngine;

public class RPGCamera : MonoBehaviour 
{
	public Transform target; 		//Target position and rotation
	public float zOffset;			//Z-axis offset
    public float xSpeed = 250.0f;	//Rotation speed on the x-axis
    public float ySpeed = 120.0f;	//Rotation speed on the y-axis
	public float zSpeed = 120.0f;	//Rotation speed on the z-axis
	public float yMinLimit;			//Least possible value of the y rotation
	public float yMaxLimit;			//Maximum possible value of the y rotation
    private float x;				//X position value
    private float y;				//Y position value

	//LateUpdate is called after all other updates are done
    void LateUpdate() 
	{    
    if (target && Input.GetMouseButton(1)) 
	{ 
		x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
		y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
     }
     
     var rotation = Quaternion.Euler(y, x, 0);
     var position = rotation * new Vector3(0.0f, 0.0f, -zOffset) + target.position;
     
     transform.rotation = rotation;
     transform.position = position;
	}
}
