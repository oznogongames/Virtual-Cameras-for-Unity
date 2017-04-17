using UnityEngine;

public class CubeMovement : MonoBehaviour 
{
	public float speed = 10.0f;	//Speed variable

	// Update is called once per frame
	void Update () 
	{
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);	//Fill the 3D vector
        transform.position += move * speed * Time.deltaTime;	//Assign the new position over time
	}
}
