using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour
{

    public InverseKinArmAtics armLeft;
    public InverseKinArmAtics armRight;
    public Camera playerCamera;
    public Transform debugSphere;

    void FixedUpdate() {
        RaycastHit raycastHit;
        Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out raycastHit, 100, 64);
        Vector3 hitLocation = raycastHit.point;
        if(hitLocation != new Vector3(0,0,0)){
            debugSphere.position = hitLocation;
            armLeft.targetPoint = hitLocation;
            armRight.targetPoint = hitLocation;
        }
    }
}
