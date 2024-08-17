using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class GrabArm : InverseKinArmAtics
{
    public GameObject grabber;
    public LayerMask grabbable;

    new void Start() {
        base.Start();
        print("Real Grab STARRRTING");
    }

    new void Update() {
        base.Update();

        grabber.SetActive(false);

        if (Physics.CheckSphere(grabber.transform.position, grabber.transform.localScale.x * 0.25f, grabbable)) { // TODO: Don't just take xscale
            grabber.SetActive(true);
        }
    }
}