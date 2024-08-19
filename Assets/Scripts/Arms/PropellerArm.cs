using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PropellerArm : InverseKinArmAtics
{
    public float sustainedForce = 40f;
    public float initialForce = 10f;
    public float floatDuration = 5f;
    public float maxRotateSpeed = 600f;
    public float timeToReachMaxRotateSpeed = 1.8f;
    public float timeToScale = 0.3f;
    private bool canFloat = false;
    private bool isFloating = false;

    private float rotateSpeed = 0;
    private float scale = 0;

    new private void Update()
    {
        base.Update();
        if (!canFloat) canFloat = CheckGrounded();
        if (!isFloating) {
            rotateSpeed -= Time.deltaTime * maxRotateSpeed / timeToReachMaxRotateSpeed;
            float targetScale = canFloat ? 0.4f : 0;
            scale = Mathf.MoveTowards(scale, targetScale, Time.deltaTime * 1 / timeToScale);
        }

        scale = Mathf.Clamp(scale, 0, 1);
        rotateSpeed = Mathf.Clamp(rotateSpeed, 0, maxRotateSpeed);

        hand.localScale = new Vector3(scale, scale, scale);
        hand.GetChild(0).Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0));
    }

    public override void Pressed() {
        if (canFloat)
        {
            canFloat = false;
            StartCoroutine(FloatCoroutine());
            playerBody.AddForce(Vector3.up * initialForce);
        }
    }

    public override void Held()
    {
        if (isFloating)
        {
            canFloat = false;
            rotateSpeed += Time.deltaTime * maxRotateSpeed / timeToReachMaxRotateSpeed;
            scale += Time.deltaTime * 1 / timeToScale;
            
            targetPoint = shoulder.position + Vector3.up * 100;

            playerBody.AddForce(Vector3.up * sustainedForce * Time.deltaTime);
        }
    }

    public override void LetGo()
    {
        canFloat = false;
        isFloating = false;
        StopAllCoroutines();
    }

    private IEnumerator FloatCoroutine()
    {
        isFloating = true;
        yield return new WaitForSeconds(floatDuration);
        isFloating = false;
    }

    private bool CheckGrounded()
    {
        return playerControllerTransform.gameObject.GetComponent<PlayerController>().getGrounded();
    }
}
