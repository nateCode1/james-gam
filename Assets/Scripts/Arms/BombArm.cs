using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BombArm : InverseKinArmAtics
{
    public GameObject bombTemplate;
    private GameObject bomb; // could be a list of bombs if we want more than one to be alive at a time
    new public void Pressed() {
        bomb = Instantiate(bombTemplate, actualHandPos, Quaternion.identity);
    }
    new public void LetGo() {
        bomb.GetComponent<Explode>().ExplodeBomb();
        Destroy(bomb);
    }
}
