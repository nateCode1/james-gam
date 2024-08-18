using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class GrabArm : InverseKinArmAtics
{
    public float grabForce = 1f;
    public float maxForce = 100f;
    public float maxDistance = 100f;
    public float drag = 10f;
    public LayerMask grabLayers;
    private Vector3 grabPoint;
    private bool isGrabbed = false;

    public override void VisualUpdate(Vector3 elbowPosition, Vector3 handPosition, float lowerArmLength) {
        if (!isGrabbed) {
            base.VisualUpdate(elbowPosition, handPosition, lowerArmLength);
        } else {
            // Places and rotates the arm segments
            upperArm.LookAt(elbowPosition);
            lowerArm.position = elbowPosition;
            lowerArm.LookAt(grabPoint); // CHANGED!

            // Places and rotates the hand
            actualHandPos = elbowPosition + lowerArmLength * (handPosition - lowerArm.position).normalized;
            hand.position = grabPoint; // CHANGED!
            if (!isGrabbed) hand.LookAt(actualHandPos + lowerArm.forward);

            oldElbowPosition = elbowPosition;
            oldTargetPoint = handPosition;
        }
    }

    public override void Pressed() {
        if (Physics.CheckSphere(actualHandPos, maxDistance, grabLayers)){
            grabPoint = actualHandPos;
            isGrabbed = true;
        }
    }
    public override void Held() {
        if (isGrabbed){
            float forceAmount = Mathf.Min(grabForce * (grabPoint - actualHandPos).magnitude, maxForce);
            playerBody.AddForce((grabPoint - actualHandPos).normalized * forceAmount * Time.deltaTime);
            playerBody.drag = drag;
        }
    }
    public override void LetGo() {
        isGrabbed = false;
        playerBody.drag = 0f;
    }
}
