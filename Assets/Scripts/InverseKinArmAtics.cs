using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InverseKinArmAtics : MonoBehaviour
{
    public float smoothing = 0.5f;
    public Vector3 targetPoint;
    public Transform shoulder;
    public Transform upperArm;
    public Transform lowerArm;
    public Transform debugSphere;
    public Transform debugSphere2;
    private Vector3 oldElbowPosition;
    private Vector3 newElbowPosition;
    private Vector3 oldHandPosition;
    private Vector3 newHandPosition;
    private Transform playerControllerTransform;

    void Start() {
        playerControllerTransform = GetComponent<Transform>();
    }

    void Update() {
        // Gets the upper arm length (side a in the cosine law)
        float upperArmLength = upperArm.GetChild(0).localScale.y * 2;
        // Gets the lower arm length (side b in the cosine law)
        float lowerArmLength = lowerArm.GetChild(0).localScale.y * 2;
        float armRadius = upperArmLength + lowerArmLength;
        // Gets the distance to the target point (side c in the cosine law)
        newHandPosition = targetPoint;
        if (Vector3.Distance(shoulder.position, newHandPosition) > armRadius){
            newHandPosition = shoulder.position + (targetPoint - shoulder.position).normalized * armRadius;
        }
        float targetDistance = (newHandPosition - shoulder.position).magnitude;
        // Calculates the shoulder angle in radians (gamma in the cosine law)
        float shoulderAngle = Mathf.Acos((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(targetDistance, 2) - Mathf.Pow(lowerArmLength, 2)) / (2 * upperArmLength * targetDistance));
        shoulderAngle += Vector3.Angle(playerControllerTransform.forward, newHandPosition - shoulder.position) * Mathf.Deg2Rad;
        // The angle is NaN when it should be exactly 180 degrees
        if (float.IsNaN(shoulderAngle)){
            shoulderAngle = Mathf.PI / 2.0f;
        }
        // Uses basic trig to calculate the elbow's position
        newElbowPosition = shoulder.position + -playerControllerTransform.right * upperArmLength * Mathf.Sin(shoulderAngle) + playerControllerTransform.forward * upperArmLength * Mathf.Cos(shoulderAngle);
        debugSphere.position = newHandPosition;
        debugSphere2.position = targetPoint;

        // Updates the arms to match the math
        // Also smooths the movement of the visual arms
        Vector3 elbowPosition = Vector3.Lerp(oldElbowPosition, newElbowPosition, smoothing);
        Vector3 handPosition = Vector3.Lerp(oldHandPosition, newHandPosition, smoothing);

        oldElbowPosition = elbowPosition;
        oldHandPosition = handPosition;

        upperArm.position = shoulder.position;
        upperArm.LookAt(elbowPosition);
        lowerArm.position = elbowPosition;
        lowerArm.LookAt(handPosition);
    }
}
