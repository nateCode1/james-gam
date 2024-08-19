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
