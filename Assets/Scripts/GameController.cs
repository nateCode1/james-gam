using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject armLeft;
    public GameObject armRight;
    void Start()
    {
        ArmsController armsController = player.GetComponent<ArmsController>();
        armsController.SwitchArm(armLeft, true);
        //armsController.SwitchArm(armRight, false);
    }
}
