using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Robot : MonoBehaviour
{
    public float rotational_speed = 0.0f;
    private Rigidbody2D _rigidbody;
    private float timeElapsed = 0.0f;

    Vector3 rotation = new Vector3(0,0,1);
    Vector3 velocity = new Vector3(0.0005f,-0.0001f,0);

    public Image image;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        image.color = new Color(0,0,0,0);
        Time.timeScale = 10f;
    }

    private void Update() {
        if(transform.position.x > 0.3) {
            image.color = Color.Lerp(image.color, Color.black, (float) 0.2 * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            }
        if(timeElapsed > 30f) {
                SceneManager.LoadScene("TitleScreen");
        }
    }

    private void FixedUpdate() {
        transform.eulerAngles -= rotation * rotational_speed;
        transform.position += velocity;
    }

}
