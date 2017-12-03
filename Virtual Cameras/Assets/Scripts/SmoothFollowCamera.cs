using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;
    public float distance = 20.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float lookAtHeight = 0.0f;
    public Rigidbody parentRigidbody;
    public float rotationSnapTime = 0.3F;
    public float distanceSnapTime;
    public float distanceMultiplier;

    private Vector3 lookAtVector;
    private float usedDistance;
    private float wantedRotationAngle;
    private float wantedHeight;
    private float currentRotationAngle;
    private float currentHeight;
    private Quaternion currentRotation;
    private Vector3 wantedPosition;
    private Vector3 velocity;

    // Use this for initialization
    void Start()
    {

        lookAtVector = new Vector3(0, lookAtHeight, 0);

    }

    // Update is called once per frame
    void Update()
    {
        wantedHeight = target.position.y + height;
        currentHeight = transform.position.y;

        wantedRotationAngle = target.eulerAngles.y;
        currentRotationAngle = transform.eulerAngles.y;

        currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref velocity.y, rotationSnapTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        wantedPosition = target.position;
        wantedPosition.y = currentHeight;

        usedDistance = Mathf.SmoothDampAngle(usedDistance, distance + (parentRigidbody.velocity.magnitude * distanceMultiplier), ref velocity.z, distanceSnapTime);

        wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

        transform.position = wantedPosition;

        transform.LookAt(target.position + lookAtVector);
    }
}
