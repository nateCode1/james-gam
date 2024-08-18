using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class FishingArm : InverseKinArmAtics
{
    public GameObject HMSBoat;
    public LayerMask groundLayers;
    public Transform player;
    public float verticalOffset = -1.0f; // Customize this value as needed

    public override void LetGo() 
    {
        if (IsAboveHMSBoat()) 
        {
            Debug.Log("Standing above HMSBoat!");
        }
    }

    private bool IsAboveHMSBoat() 
    {
        Vector3 checkPosition = player.position + new Vector3(0, -0.5f, 0);
        Vector3 boxSize = new Vector3(0.4f, 0.2f, 0.4f);

        Vector3 boatPositionWithOffset = HMSBoat.transform.position + new Vector3(0, verticalOffset, 0);

        return Physics.CheckBox(boatPositionWithOffset, boxSize, Quaternion.identity, groundLayers);
    }
}