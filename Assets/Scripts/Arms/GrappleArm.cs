using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class GrappleArm : InverseKinArmAtics
{
    public float grappleForce = 1f;
    public float maxForce = 100f;
    public float maxDistance = 100f;
    public LayerMask grappleLayers;
    private Vector3 grapplePoint;
    private bool isGrappled = false;
    public Transform beamStart;
    public Transform beamEnd; 
    public GameObject indicator;
    private GameObject attachedObject;

    public override void Pressed() {
        if (hitObject && (grappleLayers.value & (1 << hitObject.layer)) != 0 && (targetPoint - playerControllerTransform.position).magnitude < maxDistance){ // bitwise bullshit to check if the layer of the hit gameobject is allowed
            grapplePoint = targetPoint;
            attachedObject = hitObject;
            isGrappled = true;
        }
    }
    public override void Held() {
        if (isGrappled){
            float forceAmount = Mathf.Min(grappleForce * (grapplePoint - playerControllerTransform.position).magnitude, maxForce);
            playerBody.AddForce((grapplePoint - playerControllerTransform.position).normalized * forceAmount);
            playerBody.drag = 2.0f;
            targetPoint = grapplePoint;
        }
    }

    public override void VisualUpdate(Vector3 elbowPosition, Vector3 handPosition, float lowerArmLength)
    {
        if (!attachedObject) {
            LetGo();
        }
        Quaternion oldHandRotation = hand.rotation;
        base.VisualUpdate(elbowPosition, handPosition, lowerArmLength);
        if (isGrappled){
            hand.SetPositionAndRotation(grapplePoint, oldHandRotation);
        }

        if ((targetPoint - playerControllerTransform.position).magnitude > maxDistance || isGrappled) {
            indicator.SetActive(false);
        } else {
            indicator.SetActive(true);
            indicator.transform.position = targetPoint;
        }

        // Beam
        GetComponent<LineRenderer>().SetPositions(new Vector3[2] {beamStart.position, beamEnd.position});
    }

    public override void LetGo() {
        isGrappled = false;
        playerBody.drag = 0f;
    }
}
