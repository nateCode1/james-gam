using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class RockArm : InverseKinArmAtics
{
    public GameObject projectileTemplate;
    public GameObject handRock;
    public float chargeSpeed = 10f;
    public float reloadTime = 3f;
    private GameObject projectile;
    private float power = 0f;
    private float timeSinceLastShot = 0;
    private Vector3 handRockRotationSpeed = Vector3.zero;

    public override void VisualUpdate(Vector3 elbowPosition, Vector3 handPosition, float lowerArmLength)
    {
        base.VisualUpdate(elbowPosition, handPosition, lowerArmLength);

        // this isn't totally visual... sorry!
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > reloadTime && !handRock.activeSelf) {
            handRock.SetActive(true);
        }
    }

    public override void Held() {
        if (handRock.activeSelf) {
            // charges the shot power
            power += chargeSpeed * Time.deltaTime;
            // makes the model spin faster and faster
            handRockRotationSpeed += chargeSpeed * Time.deltaTime * new Vector3(1, 1, 1); // arbitrary rotation direction teehee
        }
        // updates the model
        handRock.transform.Rotate(handRockRotationSpeed);
    }

    public override void LetGo() {
        if (handRock.activeSelf){
            // Deletes the old rock
            if (projectile) {
                Destroy(projectile);
            }
            // Adds new projectile
            projectile = Instantiate(projectileTemplate, actualHandPos, Quaternion.identity);
            Vector3 force = power * lowerArm.forward.normalized;
            // Changes the projectile's force and angular velocity according to how long the arm was charged
            projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
            projectile.GetComponent<Rigidbody>().angularVelocity = -handRockRotationSpeed;
            // Resets so many variables
            timeSinceLastShot = 0f;
            power = 0f;
            handRock.SetActive(false);
            handRockRotationSpeed = Vector3.zero;
        }
    }
    void OnDestroy() {
        Destroy(projectile);
    }
}