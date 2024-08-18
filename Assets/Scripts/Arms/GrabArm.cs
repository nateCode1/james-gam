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
    public float snatchTime = 0.3f;
    public float snatchActuation = 45;
    public LayerMask grabLayers;
    private Vector3 grabPoint;
    private bool isGrabbed = false;
    [Header ("Beam")]
    public Transform beamStart;
    public Transform beamEnd;

    private float snatchFactor = 0;

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

        if (!isGrabbed) snatchFactor -= Time.deltaTime * snatchActuation / snatchTime;
        snatchFactor = Mathf.Clamp(snatchFactor, 0, snatchActuation);

        // GRABBB
        for (int i = 0; i < 3; i++) {
            Transform c = hand.GetChild(i);
            if (i == 0) c.localEulerAngles = new Vector3(snatchFactor, 0, 0);
            else if (i == 1) c.localEulerAngles = new Vector3(0, snatchFactor, 0);
            else if (i == 2) c.localEulerAngles = new Vector3(0, -snatchFactor, 0);
            else if (i == 3) c.localEulerAngles = new Vector3(-snatchFactor, 0, 0);
        }

        // Beam
        GetComponent<LineRenderer>().SetPositions(new Vector3[2] {beamStart.position, beamEnd.position});
    }

    public override void Pressed() {
        if (Physics.CheckSphere(actualHandPos, maxDistance, grabLayers)){
            grabPoint = actualHandPos;
            isGrabbed = true;
        }
    }
    public override void Held() {
        if (isGrabbed){
            snatchFactor += Time.deltaTime * snatchActuation / snatchTime;
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
