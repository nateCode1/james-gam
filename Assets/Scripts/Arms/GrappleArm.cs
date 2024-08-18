using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class GrappleArm : InverseKinArmAtics
{
    public float grappleForce = 100f;
    private Vector3 grapplePoint;
    public override void Pressed() {
        grapplePoint = targetPoint;
    }
    public override void Held() {
        playerBody.AddForce((grapplePoint - playerControllerTransform.position).normalized * grappleForce);
        targetPoint = grapplePoint;
    }
}
