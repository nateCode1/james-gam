using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    [Header ("Texas")]
    public Texture2D normal;
    public Texture2D blink;
    public Texture2D ded;
    public Texture2D[] dizzy;

    public Material faceMat;

    [Header ("Adjustable")]
    public float blinkWait = 5f;
    public float blinkDuration = 1.5f;
    public float maxAngularVel = 10f;
    public float dizzyFrameTime = 0.15f;
    public float stayDizzyFor = 1.5f;
    public float MaxVelChange = 11f;
    public float stayDedFor = 4f;

    private Rigidbody rb;
    private int dizzyIndex = 0;

    private bool wasDizzy = false;
    private bool dizzyB = false;
    private bool wasDed = false;
    private bool dedB = false;

    private Vector3 oldVel = new Vector3(0,0,0);

    void Start()
    {
        faceMat.SetTexture(Shader.PropertyToID("_Face"), normal);
        StartCoroutine(BlinkCoroutine());
        StartCoroutine(DizzyCoroutine());
        rb = transform.parent.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        wasDizzy = false;
        wasDed = false;
        if ((oldVel - rb.velocity).magnitude > MaxVelChange || dedB) {
            wasDed = true;
            faceMat.SetTexture(Shader.PropertyToID("_Face"), ded);
        }
        else if (wasDed) StartCoroutine(DedCDCoroutine());
        else if (rb.angularVelocity.magnitude > maxAngularVel || dizzyB) {
            wasDizzy = true;
            faceMat.SetTexture(Shader.PropertyToID("_Face"), dizzy[dizzyIndex]);
        }
        else if (wasDizzy) StartCoroutine(DizzyCDCoroutine());

        oldVel = rb.velocity;
    }

    private IEnumerator DizzyCoroutine()
    {

        yield return new WaitForSeconds(dizzyFrameTime);
        dizzyIndex++;
        dizzyIndex = dizzyIndex % dizzy.Length;
        StartCoroutine(DizzyCoroutine());
    }

    private IEnumerator DizzyCDCoroutine()
    {
        dizzyB = true;
        yield return new WaitForSeconds(stayDizzyFor);
        dizzyB = false;
    }
    private IEnumerator DedCDCoroutine()
    {
        dedB = true;
        yield return new WaitForSeconds(stayDedFor);
        dedB = false;
    }

    private IEnumerator BlinkCoroutine()
    {

        yield return new WaitForSeconds(blinkWait);
        faceMat.SetTexture(Shader.PropertyToID("_Face"), blink);
        StartCoroutine(UnblinkCoroutine());
    }

    private IEnumerator UnblinkCoroutine()
    {

        yield return new WaitForSeconds(blinkDuration);
        faceMat.SetTexture(Shader.PropertyToID("_Face"), normal);
        StartCoroutine(BlinkCoroutine());
    }
}
