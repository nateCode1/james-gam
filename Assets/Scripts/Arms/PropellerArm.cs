@@ -6,30 +6,23 @@ public class PropellerArm : InverseKinArmAtics
    public float propellerForce = 40f;
    public float floatDuration = 5f;
    public float upwardSpeed = 1f;
    private bool canFloat = true;
    private bool isFloating = false;
    bool canFloat = true;

    private void OnCollisionEnter(Collision collision)
    private void Update()
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canFloat = true;
        }
    }

    public override void Pressed()
    {
        
        canFloat = checkGrounded();
    }

    public override void Held()
    {
        if (canFloat)
        if (canFloat && !isFloating)
        {
            StartCoroutine(FloatCoroutine());
        }

        if (isFloating)
        {
            if (!isFloating)
            {
                StartCoroutine(FloatCoroutine());
            }
            Vector3 upwardMovement = Vector3.up * upwardSpeed;
            playerBody.AddForce(upwardMovement * propellerForce);
        }

@@ -37,14 +30,19 @@ public class PropellerArm : InverseKinArmAtics

    public override void LetGo()
    {
        
        isFloating = false;
        StopAllCoroutines();
    }

    private IEnumerator FloatCoroutine()
    {
        isFloating = true;
        canFloat = false;
        yield return new WaitForSeconds(floatDuration);
        isFloating = false;
    }
}

    private bool checkGrounded()
    {
        return playerControllerTransform.GetComponent<PlayerController>().getGrounded();
    }
}