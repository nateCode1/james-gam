using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class FishingArm : InverseKinArmAtics
{
    private GameObject HMSBoat;
    public LayerMask playerLayer;
    public float verticalOffset = -1.0f;
    public string sceneNameToLoad;
    private bool isMinigameActive = false;

    new void Start(){
        base.Start();
        HMSBoat = GameObject.Find("HMSBoat");
    }

    new void Update() {
        base.Update();

        if (PlayerPrefs.GetInt("FishingDone") == 1) {
            PlayerPrefs.SetInt("FishingDone", 0);
            isMinigameActive = false;
        }

        if (PlayerPrefs.GetInt("ArmFlyOff") == 1) {
            PlayerPrefs.SetInt("ArmFlyOff", 0);
            ArmsController ac = GameObject.Find("GameController").GetComponent<GameController>().player.GetComponent<ArmsController>();
            if (ac.leftArm && ac.leftArm.GetComponent<FishingArm>()) ac.leftArm = null;
            if (ac.rightArm && ac.rightArm.GetComponent<FishingArm>()) ac.rightArm = null;
            GameObject dropped = Instantiate(armItem, transform.position, Quaternion.Euler(0,0,0));
            Rigidbody droppedRb = dropped.AddComponent<Rigidbody>();
            droppedRb.AddForce(new Vector3(0.3f, 1, 1) * 1300);
            droppedRb.AddTorque(new Vector3(1, 1, 1) * 700);
            Destroy(dropped, 15f);
            Destroy(gameObject);
        }
    }

    public override void Pressed() 
    {
        if (IsAboveHMSBoat()) 
        {
            LoadSceneAdditively();
            print("Yay");
        }
    }

    private bool IsAboveHMSBoat() 
    {
        Vector3 boxSize = new Vector3(1f, 1f, 1f);
        Vector3 placeWherePlayerShouldBe = HMSBoat.transform.position + new Vector3(0, verticalOffset, 0);

        return Physics.CheckBox(placeWherePlayerShouldBe, boxSize, Quaternion.identity, playerLayer);
    }

    private void LoadSceneAdditively()
    {
        if (!isMinigameActive){
            SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Additive);
            isMinigameActive = true;
        }
    }
}
