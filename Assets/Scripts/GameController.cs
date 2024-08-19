using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    public GameObject player;
    public GameObject armLeft;
    public GameObject armRight;
    public int totalNumCollectables;
    public int currentCollectables = 0;

    private GameObject menuRef;

    void Start()
    {
        menuRef = Instantiate(menu);
        menuRef.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = currentCollectables + "/" + totalNumCollectables;

        ArmsController armsController = player.GetComponent<ArmsController>();
        armsController.SwitchArm(armLeft, true);
        armsController.SwitchArm(armRight, false);
    }

    public void Collect() {
        currentCollectables++;
        menuRef.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = currentCollectables + "/" + totalNumCollectables;
    }
}
