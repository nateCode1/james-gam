using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InverseKinArmAtics : MonoBehaviour
{
    public float smoothing = 0.0f;
    public Vector3 targetPoint;
    public Transform shoulder;
    public Transform upperArm;
    public Transform lowerArm;
    public Transform debugSphere;
    public Transform debugSphere2;
    private Vector3 oldElbowPosition;
    private Vector3 oldHandPosition;
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
        Vector3 newHandPosition = targetPoint - shoulder.position; // ROUNDING ERROR!!!!!!!!!!!!!!
        if (newHandPosition.magnitude > armRadius){
            newHandPosition = newHandPosition.normalized * armRadius;
        }
        float targetDistance = newHandPosition.magnitude;
        // Calculates the shoulder angle in radians (gamma in the cosine law)
        float shoulderAngle = Mathf.Acos((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(targetDistance, 2) - Mathf.Pow(lowerArmLength, 2)) / (2 * upperArmLength * targetDistance));
        shoulderAngle += Vector3.Angle(playerControllerTransform.forward, newHandPosition) * Mathf.Deg2Rad; // TRY CHANING TO VECTOR3.FORWARD!!!
        // The angle is NaN when it should be exactly 180 degrees
        if (float.IsNaN(shoulderAngle)){
            shoulderAngle = Mathf.PI / 2.0f;
        }
        // Uses basic trig to calculate the elbow's position
        Vector3 newElbowPosition = -playerControllerTransform.right * upperArmLength * Mathf.Sin(shoulderAngle) + playerControllerTransform.forward * upperArmLength * Mathf.Cos(shoulderAngle);
        
        if (debugSphere) {
            debugSphere.position = shoulder.position + newHandPosition;
        }
        if (debugSphere2) {
            debugSphere2.position = targetPoint;
        }

        // Updates the arms to match the math
        // Also smooths the movement of the visual arms
        Vector3 elbowPosition = Vector3.Lerp(shoulder.position + newElbowPosition, oldElbowPosition, smoothing);
        Vector3 handPosition = Vector3.Lerp(shoulder.position + newHandPosition, oldHandPosition, smoothing);

        upperArm.LookAt(elbowPosition);
        lowerArm.position = elbowPosition;
        lowerArm.LookAt(handPosition);

        oldElbowPosition = elbowPosition;
        oldHandPosition = handPosition;
    }

    //deprecated
    Vector3 RoundVector3 (Vector3 toRound) {
        Vector3 toReturn = new Vector3();
        toReturn.x = Mathf.Round(toRound.x * 100) / 100;
        toReturn.y = Mathf.Round(toRound.y * 100) / 100;
        toReturn.z = Mathf.Round(toRound.z * 100) / 100;
        return toReturn;
    }
}
