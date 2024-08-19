using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject pickupParticle;
    public GameObject gameController;

    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();

        if (gameController == null) gameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 60));

        if (Physics.CheckBox(transform.position + bc.center, 0.5f * new Vector3(bc.size.x*transform.localScale.x, bc.size.y*transform.localScale.y, bc.size.z*transform.localScale.z), Quaternion.identity, playerLayer)) {
            Instantiate(pickupParticle, transform.position, Quaternion.identity);
            if (gameController != null) gameController.GetComponent<GameController>().Collect();
            Destroy(gameObject);
        }
    }
}
