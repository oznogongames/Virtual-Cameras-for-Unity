using UnityEngine;

public class MTPCamera : MonoBehaviour
{
	public float zOffset;			//Z-axis offset
    public float xSpeed = 250.0f;	//Rotation speed on the x-axis
    public float ySpeed = 120.0f;	//Rotation speed on the y-axis
	public float zSpeed = 120.0f;	//Rotation speed on the z-axis
	public float yMinLimit;			//Least possible value of the y rotation
	public float yMaxLimit;			//Maximum possible value of the y rotation
    private float x;				//X position value
    private float y;				//Y position value
	
	//LateUpdate is called after all other updates are done
	void LateUpdate () 
	{
		if(Input.GetMouseButtonDown(0)) 
 		{
    		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit;

    		if(Physics.Raycast(ray, out hit))
    		{
         		gameObject.transform.position = hit.transform.position;
     		}
 		}

		if (Input.GetMouseButton(1)) 
		{ 
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        	y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
     	}

		var rotation = Quaternion.Euler(y, x, 0);
     
     	transform.rotation = rotation;
	}
}
