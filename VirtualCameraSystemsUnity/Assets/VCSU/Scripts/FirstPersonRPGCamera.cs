using UnityEngine;

public class FirstPersonRPGCamera : MonoBehaviour 
{
	public Transform target;            //Position of the target
    private Vector3 offset;
    public float yOffset = 1.5f;        //Offset of camera position on the Y-axis
    public float zOffset = 0.0f;        //Offset of camera position on the Y-axis          
    public float xSpeed = 250.0f;       //Speed of change on the X-axis
    public float ySpeed = 120.0f;       //Speed of change on the Y-axis
	public float yMinLimit = -90.0f;    //Minimum possible Y-value
	public float yMaxLimit = 90.0f;     //Maximum possible Y-value
    public float xMinLimit = -90.0f;    //Minimum possible Y-value
	public float xMaxLimit = 90.0f;     //Maximum possible Y-value
    private float x;                    //X-value
    private float y;                    //Y-value

    public void RPGCameraSetup()        //Method for initialization of the camera
	{
        transform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z + zOffset);    //Update position with parameters from above
    }
   
    //Use this for initialization
    void Start () 
	{
        if(target == null)  //Check for a target
		{
			Debug.LogWarning("VCSU ERROR: no target found!");
		}

    	RPGCameraSetup();   //Load the initialization
    }
   
   //Update is called once per frame
    void Update()
	{
    }

	//LateUpdate is called after all other updates are done
    void LateUpdate() 
	{    
        offset = new Vector3(0.0f, yOffset,zOffset);
        
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f; //X-axis value
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f; //Y-axis value
       
	    y = Mathf.Clamp(y, yMinLimit, yMaxLimit);       //Y-axis rotation limit
        x = Mathf.Clamp(x, xMinLimit, xMaxLimit);       //X-axis rotation limit
           
        Quaternion rotation = Quaternion.Euler(y, x, 0);    //Euler angles for camera rotation/orientation
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, 0.0f) + target.position + offset;   //Update the transform position with the input
       
        transform.rotation = rotation;   //Assign the transform rotation
        transform.position = position;   //Assign the transform position
    }
}
