using UnityEngine;

public class FirstPersonRPGCamera : MonoBehaviour 
{
	public Transform target;            //Position of the target
    public float walkOffset;            //Offset of camera position in walking state
    public float runOffset;             //Offset of camera position in running state
    public float yOffset;               //Offset of camera position on the Y-axis          
    public float xSpeed = 250.0f;       //Speed of change on the X-axis
    public float ySpeed = 120.0f;       //Speed of change on the Y-axis
	public float yMinLimit = -90.0f;    //Minimum possible Y-value
	public float yMaxLimit = 90.0f;     //Maximum possible Y-value
   
    private Transform _myTransform;     //This gameObject's transform
    private float x;                    //X-value
    private float y;                    //Y-value
    private bool camButtonDown = false; //Check for mouse input

    public void RPGCameraSetup()        //Method for initialization of the camera
	{
        _myTransform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z - walkOffset);    //Update position with parameters from above
        _myTransform.LookAt(target);    //Update the camera orientation with the target position
    }
   
    //Use this for initialization
    void Start () 
	{
        if(target == null)  //Check for a target
		{
			Debug.LogWarning("VCSU ERROR: no target found!");
		}
    	
		_myTransform = transform; //Update transform

    	RPGCameraSetup();   //Load the initialization
    }
   
   //Update is called once per frame
    void Update()
	{
        if(Input.GetMouseButtonDown(1)) //Check for right mouse button press
		{
            camButtonDown = true;
        }

        if(Input.GetMouseButtonUp(1))  //Check for right mouse button release
		{
            camButtonDown = false;
        }
    }

	//LateUpdate is called after all other updates are done
    void LateUpdate() 
	{    
    if(camButtonDown)
	{  
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f; //X-axis value
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f; //Y-axis value
       
	    y = Mathf.Clamp(y, yMinLimit, yMaxLimit);       //Y-axis rotation limit
           
        Quaternion rotation = Quaternion.Euler(y, x, 0);    //Euler angles for camera rotation/orientation
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -walkOffset) + target.position;   //Update the transform position with the input
       
        _myTransform.rotation = rotation;   //Assign the transform rotation
        _myTransform.position = position;   //Assign the transform position
        }
        else 
		{
            _myTransform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z - walkOffset);    //Reset values to initialization
            _myTransform.LookAt(target);
            x = 0;
            y = 0;
        }
    }
}
