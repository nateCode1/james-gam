using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class InverseKinArmAtics : MonoBehaviour
{
    public float smoothing = 0.0f;
    public Vector3 targetPoint;
    protected Vector3 oldTargetPoint;
    public Transform shoulder;
    public Transform upperArm;
    public Transform lowerArm;
    public Transform hand;
    protected Vector3 oldElbowPosition;
    protected Transform playerControllerTransform;
    public  Vector3 actualHandPos;
    public Rigidbody playerBody;
    public GameObject hitObject;
    protected float armRadius;

    protected void Start() {
        playerControllerTransform = GetComponent<Transform>();
        oldElbowPosition = Vector3.zero;
        oldTargetPoint = Vector3.zero;
    }

    protected void Update() {
        // Gets the upper arm length (side a in the cosine law)
        float upperArmLength = upperArm.GetChild(0).localScale.y * 2;
        // Gets the lower arm length (side b in the cosine law)
        float lowerArmLength = lowerArm.GetChild(0).localScale.y * 2;
        // Gets the total distance from the shoulder to the point (side c in the cosine law)
        float distanceToTarget = Mathf.Min((targetPoint - shoulder.position).magnitude, upperArmLength + lowerArmLength);
        // Calculates the angle the shoulder makes relative to side c using the law of cosines
        float shoulderAngle = (Mathf.PI / 2.0f) - Mathf.Acos((upperArmLength * upperArmLength - lowerArmLength * lowerArmLength + distanceToTarget * distanceToTarget) / (2 * upperArmLength * distanceToTarget));

        // Gets the position of the elbow in the 2d plane given by the shoulder, the target point, and camera left.
        Vector2 elbowPos2d = new Vector2(upperArmLength * Mathf.Sin(shoulderAngle), upperArmLength * Mathf.Cos(shoulderAngle));
        // Gets the position of the elbow in 3d
        // First, get the basis vectors of the plane
        // Define one basis as the direction from the shoulder to the target point
        // Another vector in the plane is the leftward direction of the camera
        // Then use the cross product to find the other basis
        Vector3 basis1 = (targetPoint - shoulder.position).normalized;
        Vector3 planeNormal = Vector3.Cross(-playerControllerTransform.right, basis1).normalized;
        Vector3 basis2 = Vector3.Cross(basis1, planeNormal);
        
        // Calculates the 3d position of the elbow using the 2d position and the vertical elevation
        Vector3 newElbowPosition = basis1 * elbowPos2d.x + basis2 * elbowPos2d.y;

        // Updates the arms to match the math
        // Also smooths the movement of the visual arms
        Vector3 elbowPosition = Vector3.Lerp(shoulder.position + newElbowPosition, oldElbowPosition, smoothing);
        Vector3 handPosition = Vector3.Lerp(targetPoint, oldTargetPoint, smoothing);
        VisualUpdate(elbowPosition, handPosition, lowerArmLength);
    }

    virtual public void VisualUpdate(Vector3 elbowPosition, Vector3 handPosition, float lowerArmLength) {
        // Places and rotates the arm segments
        upperArm.LookAt(elbowPosition);
        lowerArm.position = elbowPosition;
        lowerArm.LookAt(handPosition);

        // Places and rotates the hand
        actualHandPos = elbowPosition + lowerArmLength * lowerArm.forward;
        hand.position = actualHandPos;
        hand.LookAt(actualHandPos + lowerArm.forward);

        oldElbowPosition = elbowPosition;
        oldTargetPoint = handPosition;
    }

    virtual public void Pressed() {
        // This method body is intentionally left blank since this arm has no behaviour. Override me!
    }
    virtual public void Held() {
        // This method body is intentionally left blank since this arm has no behaviour. Override me!
    }
    virtual public void LetGo() {
        // This method body is intentionally left blank since this arm has no behaviour. Override me!
    }

}
