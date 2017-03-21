using UnityEngine;

public class FollowTargetHorizontal : MonoBehaviour 
{
    public Transform target;	//Position of the target
	public float yOffset;		//Y-axis offset
	public float zOffset;		//Z-axis offset

	//Use this for initialization
	void Start ()
	{

	}
	
	//LateUpdate is called after Update() each frame
    void LateUpdate ()
	{
		transform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z + zOffset);
	}
}