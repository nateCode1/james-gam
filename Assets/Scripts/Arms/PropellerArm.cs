using System.Collections;
using UnityEngine;

public class PropellerArm : InverseKinArmAtics
{
    public float propellerForce = 40f;
    public float floatDuration = 5f;
    public float upwardSpeed = 1f;
    private bool canFloat = false;
    private bool isFloating = false;

    private void Update()
    {
        canFloat = checkGrounded();
    }

    public override void Held()
    {
        if (canFloat && !isFloating)
        {
            StartCoroutine(FloatCoroutine());
        }

        if (isFloating)
        {
            Vector3 upwardMovement = Vector3.up * upwardSpeed;
            playerBody.AddForce(upwardMovement * propellerForce);
        }
    }

    public override void LetGo()
    {
        isFloating = false;
        StopAllCoroutines();
    }

    private IEnumerator FloatCoroutine()
    {
        isFloating = true;
        yield return new WaitForSeconds(floatDuration);
        isFloating = false;
    }

    private bool checkGrounded()
    {
        return playerControllerTransform.gameObject.GetComponent<PlayerController>().getGrounded();
    }
}
