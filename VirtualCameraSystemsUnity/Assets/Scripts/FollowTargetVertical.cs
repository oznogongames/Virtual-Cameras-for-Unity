using UnityEngine;

public class FollowTargetVertical : MonoBehaviour 
{
    public Transform target;	//Position of the target
	public float xOffset;		//X-axis offset
	public float zOffset;		//Z-axis offset

	//Use this for initialization
	void Start ()
	{

	}
	
	//LateUpdate is called after Update() each frame
    void LateUpdate ()
	{
		transform.position = new Vector3(target.position.x + xOffset, target.position.y, target.position.z + zOffset);
	}
}