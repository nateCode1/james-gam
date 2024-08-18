using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class InverseKinArmAtics : MonoBehaviour
{
    public float smoothing = 0.0f;
    public Vector3 targetPoint;
    public Transform shoulder;
    public Transform upperArm;
    public Transform lowerArm;
    public Transform hand;
    private Vector3 oldElbowPosition;
    protected Vector3 oldHandPosition;
    private Transform playerControllerTransform;
    private float oldShoulderAngle;
    private  Vector3 actualHandPos;

    protected float armRadius;

    protected void Start() {
        playerControllerTransform = GetComponent<Transform>();
    }

    protected void Update() {
        // Gets the upper arm length (side a in the cosine law)
        float upperArmLength = upperArm.GetChild(0).localScale.y * 2;
        // Gets the lower arm length (side b in the cosine law)
        float lowerArmLength = lowerArm.GetChild(0).localScale.y * 2;
        armRadius = upperArmLength + lowerArmLength;
        // Gets the distance to the target point (side c in the cosine law)
        Vector3 newHandPosition = targetPoint - shoulder.position; // ROUNDING ERROR?

        float targetDistance = newHandPosition.magnitude;
        if (newHandPosition.magnitude > armRadius){
            newHandPosition = newHandPosition.normalized * armRadius;
            targetDistance = armRadius - 0.01f; // I HATE YOU!!!!!!!!!!!!
        }

        // TODO: delete all references to playerControllerTransform.forward or .right or whatever
        // Calculates the shoulder angle in radians (gamma in the cosine law)
        float shoulderAngle = Mathf.Acos((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(targetDistance, 2) - Mathf.Pow(lowerArmLength, 2)) / (2 * upperArmLength * targetDistance));

        // Shoulder Angle is only NaN when the target point is out of range or the angles happen to line up just wrong
        if (float.IsNaN(shoulderAngle)) {
            shoulderAngle = oldShoulderAngle;
        }
        oldShoulderAngle = shoulderAngle;

        // Uses COMPLICATED trig to calculate the elbow's position
        Vector3 thisForward = newHandPosition.normalized;
        Vector3 planeNormal = Vector3.Cross(shoulder.position - playerControllerTransform.position, thisForward);
        Vector3 thisLeft = Vector3.Cross(thisForward, planeNormal);
        Debug.Log("this one " + (shoulder.position - playerControllerTransform.position).ToString());
        Vector3 newElbowPosition = Mathf.Sin(shoulderAngle) * upperArmLength * thisLeft + Mathf.Cos(shoulderAngle) * upperArmLength * thisForward;

        // Updates the arms to match the math
        // Also smooths the movement of the visual arms
        Vector3 elbowPosition = Vector3.Lerp(shoulder.position + newElbowPosition, oldElbowPosition, smoothing);
        Vector3 handPosition = Vector3.Lerp(shoulder.position + newHandPosition, oldHandPosition, smoothing);
        
        actualHandPos = elbowPosition + lowerArmLength * lowerArm.forward;

        upperArm.LookAt(elbowPosition);
        lowerArm.position = elbowPosition;
        lowerArm.LookAt(handPosition);
        hand.position = actualHandPos;
        hand.LookAt(actualHandPos + lowerArm.forward);

        oldElbowPosition = elbowPosition;
        oldHandPosition = handPosition;
    }

    virtual public void Pressed() {
        Debug.Log("THIS DEFAULT ARM WAS CLICKED!!!!!!!!!");
    }
    virtual public void Held() {
        Debug.Log("THIS DEFAULT ARM WAS HELD DOWN!!!!!!!!!");
    }
    virtual public void LetGo() {
        Debug.Log("THIS DEFAULT ARM WAS LET GO!!!!!!!!!");
    }

}
