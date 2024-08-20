using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndCannonController : MonoBehaviour
{
    public GameObject trigger;
    public LayerMask playerLayer;
    public GameObject explosion;
    public Camera endCam;
    public float rotateTime = 3f;

    private bool triggered = false;
    private float initRot = 0;
    private float rotateTimer = 0;
    private SphereCollider sc;
    private bool startLaunch = false;
    private bool launched = false;

    // Start is called before the first frame update
    void Start()
    {
        sc = trigger.GetComponent<SphereCollider>();
        initRot = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (launched) return;

        triggered = Physics.CheckSphere(trigger.transform.position, sc.radius, playerLayer);

        rotateTimer += Time.deltaTime * (triggered ? 1 : -1);
        rotateTimer = Mathf.Clamp(rotateTimer, 0, rotateTime);

        transform.eulerAngles = new Vector3(Mathf.LerpAngle(initRot, 90, rotateTimer / rotateTime), transform.eulerAngles.y, transform.eulerAngles.z);

        if (rotateTimer == 3f) {
            Rigidbody rb = GameObject.Find("GameController").GetComponent<GameController>().player.transform.parent.GetComponentInChildren<Rigidbody>();
            Instantiate(explosion, transform.position, Quaternion.identity);
            rb.AddForce(Vector3.up * 10000);
            launched = true;
        }

        if (rotateTimer != 0 && !startLaunch) {
            Camera.main.enabled = false;
            endCam.enabled = true;
            startLaunch = true;
        }
    }
}
