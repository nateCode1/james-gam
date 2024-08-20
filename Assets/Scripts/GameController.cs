using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    public GameObject player;
    public GameObject armLeft;
    public GameObject armRight;
    public int totalNumCollectables;
    public int currentCollectables = 0;

    private Vector3 respawnPoint;
    private Gui guiInfo;
    private GameObject menuRef;
    private bool dying = false;
    private float dyingScreenOpacity = 0;
    private Rigidbody prb;
    private bool fishCollect = false;

    void Start()
    {
        menuRef = Instantiate(menu);
        guiInfo = menuRef.GetComponent<Gui>();

        guiInfo.collectableText.text = currentCollectables + "/" + totalNumCollectables;

        guiInfo.deathImage.color = new Color(1,1,1,0);

        //get body
        prb = player.transform.parent.GetComponentInChildren<Rigidbody>();
        respawnPoint = prb.position;

        ArmsController armsController = player.GetComponent<ArmsController>();
        if (armLeft) armsController.SwitchArm(armLeft, true);
        if (armRight) armsController.SwitchArm(armRight, false);
    }

    void Update() {
        dyingScreenOpacity += Time.deltaTime * (dying ? 2 : -2);
        dyingScreenOpacity = Mathf.Clamp(dyingScreenOpacity, 0, 1);
        guiInfo.deathImage.gameObject.SetActive(dyingScreenOpacity != 0);
        guiInfo.deathImage.color = new Color(1,1,1,dyingScreenOpacity);
        if(!fishCollect && PlayerPrefs.GetInt("FishCollect", 0) >= 1) {
            collectables += 1;
        }
    }

    public void Collect() {
        currentCollectables++;
        guiInfo.collectableText.text = currentCollectables + "/" + totalNumCollectables;
    }

    public void Die() {
        if (dying) return;
        dying = true;
        StartCoroutine(UndieCoroutine());
    }

    private IEnumerator UndieCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        prb.position = respawnPoint; // respawn
        prb.velocity = Vector3.zero;
        prb.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        dying = false;
    }
}
