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
    private GameObject menuRef;
    private bool dying = false;
    private float dyingScreenOpacity = 0;
    private Image deathScreen;

    void Start()
    {
        menuRef = Instantiate(menu);
        menuRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentCollectables + "/" + totalNumCollectables;
        deathScreen = menuRef.transform.GetChild(3).GetComponent<Image>();

        deathScreen.color = new Color(1,1,1,0);

        //get body
        respawnPoint = player.transform.GetChild(0).position;
        print(respawnPoint);

        ArmsController armsController = player.GetComponent<ArmsController>();
        if (armLeft) armsController.SwitchArm(armLeft, true);
        if (armRight) armsController.SwitchArm(armRight, false);
    }

    void Update() {
        dyingScreenOpacity += Time.deltaTime * (dying ? 2 : -2);
        dyingScreenOpacity = Mathf.Clamp(dyingScreenOpacity, 0, 1);
        deathScreen.color = new Color(1,1,1,dyingScreenOpacity);
    }

    public void Collect() {
        currentCollectables++;
        menuRef.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = currentCollectables + "/" + totalNumCollectables;
    }

    public void Die() {
        if (dying) return;
        dying = true;
        StartCoroutine(UndieCoroutine());
    }

    private IEnumerator UndieCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        player.transform.GetChild(0).position = respawnPoint; // respawn
        player.transform.GetChild(0).GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.GetChild(0).GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        dying = false;
    }
}
