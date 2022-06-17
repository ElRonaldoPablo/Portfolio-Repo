using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    GamepadTest gamepadTest;
    PlayerMovementControls pMovementControls;
    public Rigidbody2D rb2d;

    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool canDash = true;
    [SerializeField] private float dashSpeed = 0.0f;

    [SerializeField] private float dashCooldownTimer = 0.0f;
    private float dashCooldownTime = 0.0f;


    private float time = 0.0f;
    [SerializeField] private float dashTimer = 1.0f;

    public float testNumber = 0.1f;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        pMovementControls = GetComponent<PlayerMovementControls>();
        gamepadTest = new GamepadTest();
    }

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (pMovementControls.dashValue >= 1.0f && canDash)
        {
            isDashing = true;
        }
        //else if (pMovementControls.dashValue == 0.0f)
        //{
        //    isDashing = false;
        //}

        if (!canDash)
        {
            time = dashTimer;

            dashCooldownTime -= Time.deltaTime;

            if (dashCooldownTime <= 0.0f)
            {
                canDash = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        //rb2d.AddForce((Vector2.right * dashSpeed), ForceMode2D.Impulse);
        //time -= Time.deltaTime;

        //if (time <= 0.0f)
        //{
        //    isDashing = false;
        //    rb2d.velocity = Vector2.zero;
        //    canDash = false;
        //}

        //yield return new WaitForSeconds(0.01f);
        //dashCooldownTime = dashCooldownTimer;

        rb2d.AddForce((Vector2.right * dashSpeed), ForceMode2D.Impulse);
        yield return new WaitForSeconds(testNumber);

        isDashing = false;
        rb2d.velocity = Vector2.zero;
        canDash = false;

        yield return new WaitForSeconds(0.01f);
        dashCooldownTime = dashCooldownTimer;

    }
}
