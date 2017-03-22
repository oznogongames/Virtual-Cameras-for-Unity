using UnityEngine;

public class OrbitAroundTargetCamera : MonoBehaviour 
{
	public GameObject target;			//The target object
    public float speedMod = 10.0f;		//A speed modifier value
    private Vector3 point;				//The 3D vector for the target position
   
   //Use this for initialization
    void Start () 
	{
        point = target.transform.position;		//Get the position of the target
        transform.LookAt(point);				//Makes the camera look to it
    }
   
   //Update is called once per frame
    void Update () 
	{
		//Makes the camera orbit around the point, rotating around its Y axis, 20 degrees per second times the speed modifier.
        transform.RotateAround(point, new Vector3(0.0f,1.0f,0.0f), 20 * Time.deltaTime * speedMod);
    }
}
