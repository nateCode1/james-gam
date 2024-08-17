using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmsController : MonoBehaviour
{
    public GameObject leftArm;
    public GameObject rightArm;
    public Transform leftShoulder;
    public Transform rightShoulder;
    public Camera playerCamera;
    public Transform debugSphere;
    public LayerMask target;

    void FixedUpdate() {
        RaycastHit raycastHit;
        Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out raycastHit, 100, target);
        Vector3 hitLocation = raycastHit.point;
        if(hitLocation != new Vector3(0,0,0)){
            debugSphere.position = hitLocation;
            if (!leftArm.IsUnityNull()){
                InverseKinArmAtics leftArmAtics = leftArm.GetComponent<InverseKinArmAtics>();
                leftArmAtics.targetPoint = hitLocation;
            }
            if (!rightArm.IsUnityNull()){
                InverseKinArmAtics rightArmAtics = rightArm.GetComponent<InverseKinArmAtics>();
                rightArmAtics.targetPoint = hitLocation;
            }
        }
    }

    void Update() {
        DetectActions();
    }

    public void SwitchArm(GameObject newArm, bool isLeftArm) {
        if (isLeftArm) {
            Destroy(leftArm);
            leftArm = Instantiate(newArm, leftShoulder.position, Quaternion.identity, leftShoulder);
        } else {
            Destroy(rightArm);
            rightArm = Instantiate(newArm, rightShoulder.position, Quaternion.Euler(new Vector3(0, 180, 0)), rightShoulder);
        }
    }

    void DetectActions() {
        if (Input.GetMouseButtonDown(0) && leftArm) {
            leftArm.GetComponent<InverseKinArmAtics>().activate();
        }
        if (Input.GetMouseButton(0) && leftArm) {
            leftArm.GetComponent<InverseKinArmAtics>().held();
        }
        if (Input.GetMouseButtonDown(1) && rightArm) {
            rightArm.GetComponent<InverseKinArmAtics>().activate();
        }
        if (Input.GetMouseButton(1) && rightArm) {
            rightArm.GetComponent<InverseKinArmAtics>().held();
        }
    }
}