using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject arm;
    void Start()
    {
        ArmsController armsController = player.GetComponent<ArmsController>();
        armsController.SwitchArm(arm, false);
    }
}
