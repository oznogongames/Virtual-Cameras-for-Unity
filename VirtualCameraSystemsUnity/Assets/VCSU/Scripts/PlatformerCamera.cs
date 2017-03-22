using UnityEngine;

public class PlatformerCamera : MonoBehaviour 
{
	public Transform target;				//Position of the target
	public float smoothingFactor = 2.0f;	//Smoothing factor
	private Vector3 newPositionVector;		//3D vector for desired position

	// Use this for initialization
	void Start () 
	{
		if(target == null)  //Check for a target
		{
			Debug.LogWarning("VCSU ERROR: no target found!");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		newPositionVector = new Vector3(target.transform.position.x, target.transform.position.y, gameObject.transform.position.z);	//Fill the 3D vector
   		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPositionVector, smoothingFactor * Time.deltaTime);	//Assign new position based on input
	}
}
