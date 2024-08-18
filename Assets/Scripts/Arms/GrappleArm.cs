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
    public override void Pressed() {
        if (hitObject && (grappleLayers.value & (1 << hitObject.layer)) != 0 && (grapplePoint - playerControllerTransform.position).magnitude < maxDistance){ // bitwise bullshit to check if the layer of the hit gameobject is allowed
            grapplePoint = targetPoint;
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
    public override void LetGo() {
        isGrappled = false;
        playerBody.drag = 0f;
    }
}
