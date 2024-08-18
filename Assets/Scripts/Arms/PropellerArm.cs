using System.Collections;
using UnityEngine;

public class PropellerArm : InverseKinArmAtics
{
    public float propellerForce = 40f;
    public float floatDuration = 5f;
    public float upwardSpeed = 1f;
    private bool canFloat = true;
    private bool isFloating = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canFloat = true;
        }
    }

    public override void Pressed()
    {
        
    }

    public override void Held()
    {
        if (canFloat)
        {
            if (!isFloating)
            {
                StartCoroutine(FloatCoroutine());
            }
            Vector3 upwardMovement = Vector3.up * upwardSpeed;
            playerBody.AddForce(upwardMovement * propellerForce);
        }
    }

    public override void LetGo()
    {
        
    }

    private IEnumerator FloatCoroutine()
    {
        isFloating = true;
        canFloat = false;
        yield return new WaitForSeconds(floatDuration);
        isFloating = false;
    }
}
