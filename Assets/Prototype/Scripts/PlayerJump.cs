using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    GamepadTest gamepadTest;
    PlayerMovementControls pMovementControls;
    PlayerMovement pMovement;
    [SerializeField] Rigidbody2D rb2d;

    public bool isJumping = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        pMovementControls = GetComponent<PlayerMovementControls>();
        pMovement = GetComponent<PlayerMovement>();
        gamepadTest = new GamepadTest();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pMovement.isGrounded && pMovementControls.jumpValue >= 1)
        {
            StartCoroutine(Jump());
        }
        else if (!pMovement.isGrounded && pMovementControls.jumpValue <= 0)
        {
            StartCoroutine(StopJump());
        }
    }

    IEnumerator Jump()
    {
        if (pMovement.isGrounded)
        {
            rb2d.AddForce(new Vector2(pMovement.rb2d.velocity.x, 2.0f) * 800.0f * Time.deltaTime, ForceMode2D.Impulse);
            print(pMovementControls.jumpValue);
            isJumping = true;

            yield return new WaitForSeconds(0.01f);
        }
        
    }

    IEnumerator StopJump()
    {
        if (!pMovement.isGrounded)
        {
            isJumping = false;
            print(pMovementControls.jumpValue);
            yield return new WaitForSeconds(0.01f);
        }
    }

    #region Enable & Disable Functions

    private void OnEnable()
    {
        gamepadTest.Player.Enable();
    }

    private void OnDisable()
    {
        gamepadTest.Player.Disable();
    }

    #endregion
}
