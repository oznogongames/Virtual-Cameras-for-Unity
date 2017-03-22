using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public float mainSpeed = 100.0f;   //Regular speed variable
    public float shiftAdd = 100.0f;    //Additional force variable
    public float maxShift = 500.0f;    //Maximum additional force variable
    public float camSens = 0.25f;      //Mouse sensitivity variable
    private Vector3 previousMousePosition = new Vector3(0.0f, 0.0f, 0.0f); //3D vector for previous mouse position
    private float totalRun = 1.0f;     //Total runtime variable

    void Update()
    {
        //Mouse input
        previousMousePosition = Input.mousePosition - previousMousePosition;
        previousMousePosition = new Vector3(-previousMousePosition.y * camSens, previousMousePosition.x * camSens, 0);
        previousMousePosition = new Vector3(transform.eulerAngles.x + previousMousePosition.x, transform.eulerAngles.y + previousMousePosition.y, 0);
        transform.eulerAngles = previousMousePosition;
        previousMousePosition = Input.mousePosition;

        //Keyboard input
        Vector3 p = GetBaseInput(); //Fill the 3D vector with the desired input axis

        if (Input.GetKey(KeyCode.LeftShift))    //Check for left shift press
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);    //Limit X-axis value
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);    //Limit Y-axis value
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);    //Limit Z-axis value
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;

        if (Input.GetKey(KeyCode.Space)) //Move on X and Z-axis only
        {
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(p); //Move all axes
        }
    }

    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity; //Returns the basic values, if it's 0 then it's not active
    }
}