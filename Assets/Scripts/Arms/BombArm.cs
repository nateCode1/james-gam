using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BombArm : InverseKinArmAtics
{
    public GameObject bombTemplate;
    public GameObject handBomb;
    private GameObject bomb; // could be a list of bombs if we want more than one to be alive at a time
    public override void Pressed() {
        bomb = Instantiate(bombTemplate, actualHandPos, Quaternion.identity);
        handBomb.SetActive(false);
    }
    public override void LetGo() {
        handBomb.SetActive(true);
        bomb.GetComponent<Explode>().ExplodeBomb();
        Destroy(bomb);
    }
}
