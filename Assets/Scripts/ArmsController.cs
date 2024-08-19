using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class ArmsController : MonoBehaviour
{
    public GameObject leftArm;
    public GameObject rightArm;
    public float armSpacing = 1.0f;
    private InverseKinArmAtics leftArmAtics;
    private InverseKinArmAtics rightArmAtics;
    public Transform leftShoulder;
    public Transform rightShoulder;
    public Camera playerCamera;
    public Transform debugSphere;
    public LayerMask target;
    public int rayTestLength = 100;
    public float maxPickupDistance = 5.0f;
    private GameObject hitObject;

    void FixedUpdate() {
        RaycastHit raycastHit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out raycastHit, rayTestLength, target);
        Vector3 hitLocation = raycastHit.point;
        hitObject = hitLocation != new Vector3(0,0,0) ? raycastHit.collider.transform.gameObject : null;
        if (hitLocation == new Vector3(0,0,0)) {
            hitLocation = leftShoulder.position + ray.direction.normalized * rayTestLength;
        }
        debugSphere.position = hitLocation;
        if (!leftArm.IsUnityNull()){
            leftArmAtics.targetPoint = hitLocation + -leftShoulder.right * armSpacing / 2.0f;
            leftArmAtics.hitObject = hitObject;
        }
        if (!rightArm.IsUnityNull()){
            rightArmAtics.targetPoint = hitLocation + rightShoulder.right * armSpacing / 2.0f;
            rightArmAtics.hitObject = hitObject;
        }
    }

    void Update() {
        if (hitObject && hitObject.layer == 9) { // 9 corresponds to arm items. if you hover over an arm, the logic should switch to pickup logic
            DetectArmSwitch();
        } else {
            DetectActions();
        }
    }

    public GameObject SwitchArm(GameObject newArm, bool isLeftArm) {
        if (isLeftArm) {
            GameObject oldArmItem = null;
            if (leftArm) {
                oldArmItem = leftArm.GetComponent<InverseKinArmAtics>().armItem;
                Destroy(leftArm);
                if (leftArmAtics is GrappleArm || leftArmAtics is GrabArm || leftArmAtics is BombArm){
                    leftArmAtics.LetGo();
                }
            }
            leftArm = Instantiate(newArm, leftShoulder.position, Quaternion.identity, leftShoulder);
            leftArmAtics = leftArm.GetComponent<InverseKinArmAtics>();
            leftArmAtics.playerBody = transform.parent.GetChild(0).GetComponent<Rigidbody>();
            return oldArmItem;
        } else {
            GameObject oldArmItem = null;
            if (rightArm) {
                oldArmItem = rightArm.GetComponent<InverseKinArmAtics>().armItem;
                Destroy(rightArm);
                if (rightArmAtics is GrappleArm || rightArmAtics is GrabArm || rightArmAtics is BombArm){
                    rightArmAtics.LetGo();
                }
            }
            rightArm = Instantiate(newArm, rightShoulder.position, Quaternion.Euler(new Vector3(0, 180, 0)), rightShoulder);
            rightArmAtics = rightArm.GetComponent<InverseKinArmAtics>();
            rightArmAtics.playerBody = transform.parent.GetChild(0).GetComponent<Rigidbody>();
            return oldArmItem;
        }
    }

    void DetectArmSwitch() {
        // Max pickup distance
        if ((transform.position - hitObject.transform.position).magnitude > maxPickupDistance) return;

        ArmItem armItem = hitObject.GetComponent<ArmItem>();
        if (Input.GetMouseButtonDown(0)){
            GameObject oldArmItem = SwitchArm(armItem.armPrefab, true);
            if (oldArmItem) {
                Instantiate(oldArmItem, leftShoulder.position, Quaternion.identity);
            }
            Destroy(hitObject);
        } else if(Input.GetMouseButtonDown(1)) {
            GameObject oldArmItem = SwitchArm(armItem.armPrefab, false);
            if (oldArmItem) {
                Instantiate(oldArmItem, rightShoulder.position, Quaternion.identity);
            }
            Destroy(hitObject);
        }
    }

    void DetectActions() {
        if (Input.GetMouseButtonDown(0) && leftArm && leftArmAtics.isActive) {
            leftArmAtics.Pressed();
        }
        if (Input.GetMouseButton(0) && leftArm && leftArmAtics.isActive) {
            leftArmAtics.Held();
        }
        if (Input.GetMouseButtonUp(0) && leftArm && leftArmAtics.isActive) {
            leftArmAtics.LetGo();
        }
        if (Input.GetMouseButtonDown(1) && rightArm && rightArmAtics.isActive) {
            rightArmAtics.Pressed();
        }
        if (Input.GetMouseButton(1) && rightArm && rightArmAtics.isActive) {
            rightArmAtics.Held();
        }
        if (Input.GetMouseButtonUp(1) && rightArm && rightArmAtics.isActive) {
            rightArmAtics.LetGo();
        }
    }
}