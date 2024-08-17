using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InverseKinArmAtics : MonoBehaviour
{
    public Vector3 targetPoint;
    public Transform shoulder;
    public Transform upperArm;
    public Transform lowerArm;
    public Transform debugSphere;
    public Transform debugSphere2;
    private Vector3 elbowPosition;
    private Vector3 handPosition;
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
        handPosition = targetPoint;
        if (Vector3.Distance(shoulder.position, handPosition) > armRadius){
            handPosition = shoulder.position + (targetPoint - shoulder.position).normalized * armRadius;
        }
        float targetDistance = (handPosition - shoulder.position).magnitude;
        // Calculates the shoulder angle in radians (gamma in the cosine law)
        float shoulderAngle = Mathf.Acos((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(targetDistance, 2) - Mathf.Pow(lowerArmLength, 2)) / (2 * upperArmLength * targetDistance));
        shoulderAngle += Vector3.Angle(playerControllerTransform.forward, handPosition - shoulder.position) * Mathf.Deg2Rad;
        // The angle is NaN when it should be exactly 180 degrees
        if (float.IsNaN(shoulderAngle)){
            shoulderAngle = Mathf.PI / 2.0f;
        }
        // Uses basic trig to calculate the elbow's position
        elbowPosition = shoulder.position + -playerControllerTransform.right * upperArmLength * Mathf.Sin(shoulderAngle) + playerControllerTransform.forward * upperArmLength * Mathf.Cos(shoulderAngle);
        debugSphere.position = handPosition;
        debugSphere2.position = targetPoint;

        // Updates the arms to match the math
        upperArm.position = shoulder.position;
        upperArm.LookAt(elbowPosition);
        lowerArm.position = elbowPosition;
        lowerArm.LookAt(handPosition);
    }
}
