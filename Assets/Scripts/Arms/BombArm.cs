using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BombArm : InverseKinArmAtics
{
    public GameObject bombTemplate;
    public GameObject handBomb;
    public float reloadTime = 3;
    private float timeSinceLastShot = 0;
    private GameObject bomb; // could be a list of bombs if we want more than one to be alive at a time

    public override void VisualUpdate(Vector3 elbowPosition, Vector3 handPosition, float lowerArmLength){
        // this isn't totally visual... sorry!
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > reloadTime && !handBomb.activeSelf && !bomb) {
            handBomb.SetActive(true);
        }
        base.VisualUpdate(elbowPosition, handPosition, lowerArmLength);
    }

    public override void Pressed() {
        bomb = Instantiate(bombTemplate, actualHandPos, Quaternion.identity);
        handBomb.SetActive(false);
    }
    public override void LetGo() {
        if (bomb) {
            timeSinceLastShot = 0;
            bomb.GetComponent<Explode>().ExplodeBomb();
            Destroy(bomb);
        }
    }
}
