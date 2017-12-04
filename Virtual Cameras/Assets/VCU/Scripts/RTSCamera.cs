using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("Zone that will receive on-screen cursor position")]
    [Range(0f, 100f)]
    public float scrollZone = 30.0f;
    [Tooltip("Multiplier for the movement speed")]
    [Range(0f, 200f)]
    public float moveSpeed = 20.0f;
    [Tooltip("Multiplier for the zoom speed")]
    [Range(0f, 200f)]
    public float scrollSpeed = 100.0f;
    [Tooltip("Smoothing factor")]
    [Range(0f, 10f)]
    public float smoothFactor = 0.2f;
    [Tooltip("Movement limits on the X-axis")]
    public Vector2 moveLimitsX;
    [Tooltip("Movement limits on the Y-axis")]
    public Vector2 scrollLimitsY;
    [Tooltip("Movement limits on the Z-axis")]
    public Vector2 moveLimitsZ;
    [Tooltip("Whether the cursor should be hidden in playmode")]
    public bool hideCursor = false;
    [Tooltip("Whether the cursor should be locked in playmode")]
    public bool lockCursor = false;

    private Vector3 desiredPosition;
 
     // Start method is called at initiation
     void Start()
     {
        if (hideCursor)
        {
            Cursor.visible = false;
        }
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        desiredPosition = transform.position;
     }
 
     // Update is called once per frame
     void Update()
     {
         float x = 0, y = 0, z = 0;  //3D vector variables
 
         if (Input.mousePosition.x<scrollZone)
         {
             x -= moveSpeed * Time.deltaTime;
         }
         else if (Input.mousePosition.x > Screen.width - scrollZone)
         {
             x = moveSpeed * Time.deltaTime;
         }
 
         if (Input.mousePosition.y<scrollZone)
         {
             z -= moveSpeed * Time.deltaTime;
         }
         else if (Input.mousePosition.y > Screen.height - scrollZone)
         {
             z = moveSpeed * Time.deltaTime;
         }
 
         y = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime; //Scrollwheel input of the mouse for Y movement
 
         Vector3 move = new Vector3(x, -y, z) + desiredPosition;
         move.x = Mathf.Clamp(move.x, moveLimitsX.x, moveLimitsX.y);
         move.y = Mathf.Clamp(move.y, scrollLimitsY.x, scrollLimitsY.y);
         move.z = Mathf.Clamp(move.z, moveLimitsX.x, moveLimitsX.y);
         desiredPosition = move;
         transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothFactor);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
