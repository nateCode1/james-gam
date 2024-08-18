using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class ArmsController : MonoBehaviour
{
    public GameObject leftArm;
    public GameObject rightArm;
    private InverseKinArmAtics leftArmAtics;
    private InverseKinArmAtics rightArmAtics;
    public Transform leftShoulder;
    public Transform rightShoulder;
    public Camera playerCamera;
    public Transform debugSphere;
    public LayerMask target;
    public int rayTestLength = 100;
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
            leftArmAtics.targetPoint = hitLocation;
            leftArmAtics.hitObject = hitObject;
        }
        if (!rightArm.IsUnityNull()){
            rightArmAtics.targetPoint = hitLocation;
            rightArmAtics.hitObject = hitObject;
        }
    }

    void Update() {
        if (hitObject && hitObject.layer == 7) { // 7 corresponds to arm items. if you hover over an arm, the logic should switch to pickup logic
            DetectArmSwitch();
        } else {
            DetectActions();
        }
    }

    public GameObject SwitchArm(GameObject newArm, bool isLeftArm) {
        if (isLeftArm) {
            GameObject oldArm = leftArm;
            Destroy(leftArm);
            leftArm = Instantiate(newArm, leftShoulder.position, Quaternion.identity, leftShoulder);
            leftArmAtics = leftArm.GetComponent<InverseKinArmAtics>();
            leftArmAtics.playerBody = transform.parent.GetChild(0).GetComponent<Rigidbody>();
            Debug.Log(oldArm);
            return oldArm;
        } else {
            GameObject oldArm = rightArm;
            Destroy(rightArm);
            rightArm = Instantiate(newArm, rightShoulder.position, Quaternion.Euler(new Vector3(0, 180, 0)), rightShoulder);
            rightArmAtics = rightArm.GetComponent<InverseKinArmAtics>();
            rightArmAtics.playerBody = transform.parent.GetChild(0).GetComponent<Rigidbody>();
            return oldArm;
        }
    }

    void DetectArmSwitch() {
        ArmItem armItem = hitObject.GetComponent<ArmItem>();
        if (Input.GetMouseButtonDown(0)){
            GameObject oldArm = SwitchArm(armItem.armPrefab, true);
            Instantiate(oldArm.GetComponent<InverseKinArmAtics>().armItem, leftShoulder.position - new Vector3(0, -1.5f, 0), Quaternion.identity);
        } else if(Input.GetMouseButton(1)) {
            GameObject oldArm = SwitchArm(armItem.armPrefab, false);
            Instantiate(oldArm.GetComponent<InverseKinArmAtics>().armItem, rightShoulder.position - new Vector3(0, -1.5f, 0), Quaternion.identity);
        }
    }

    void DetectActions() {
        if (Input.GetMouseButtonDown(0) && leftArm) {
            leftArm.GetComponent<InverseKinArmAtics>().Pressed();
        }
        if (Input.GetMouseButton(0) && leftArm) {
            leftArm.GetComponent<InverseKinArmAtics>().Held();
        }
        if (Input.GetMouseButtonUp(0) && leftArm) {
            leftArm.GetComponent<InverseKinArmAtics>().LetGo();
        }
        if (Input.GetMouseButtonDown(1) && rightArm) {
            rightArm.GetComponent<InverseKinArmAtics>().Pressed();
        }
        if (Input.GetMouseButton(1) && rightArm) {
            rightArm.GetComponent<InverseKinArmAtics>().Held();
        }
        if (Input.GetMouseButtonUp(1) && rightArm) {
            rightArm.GetComponent<InverseKinArmAtics>().LetGo();
        }
    }
}