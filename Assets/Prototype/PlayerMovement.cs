using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[HelpURL("https://answers.unity.com/questions/1086765/any-relatively-simple-way-to-handle-2d-slopes-with.html")]
public class PlayerMovement : MonoBehaviour
{
    #region Out-Of-Inspector Variables

    GamepadTest gamepadTest;
    PlayerMovementControls pMovementControls;
    private Animator anim;

    private float dashCooldownTime = 0.0f;
    private float time = 0.0f;

    #endregion

    public Rigidbody2D rb2d;

    [Header("Analog Stick Tilt Values")]
    [SerializeField][Range(-1.0f, 1.0f)] private float walkTilt = 0.5f;
    [SerializeField][Range(-1.0f, 1.0f)] private float jogTilt = 0.5f;
    [SerializeField][Range(-1.0f, 1.0f)] private float runTilt = 0.8f;

    [Header("Movement Speed")]
    [SerializeField][Range(1.0f, 1000.0f)] private float walkSpeed = 1.0f;
    [SerializeField][Range(1.0f, 1000.0f)] private float jogSpeed = 1.0f;
    [SerializeField][Range(1.0f, 1000.0f)] private float runSpeed = 1.0f;

    [Header("Dash Properties")]
    [SerializeField] private float dashSpeed = 0.0f;
    [SerializeField] private float dashTimer = 1.0f;
    [SerializeField] private float dashCooldownTimer = 0.0f;

    private bool isDashing = false;
    private bool canDash = true;
    private bool dashButtonReleased = false;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 0.0f;
    [SerializeField] private int extraJumps = 2;
    private float fullJump = 1.0f;
    private float jumpTimeCounter;
    public float jumpTime = 1.0f;

    private bool isJumping = false;
    private bool justJumped = false;
    private bool canDoubleJump = false;
    private bool activateDoubleJump = false;
    private bool dJumpHaloOut = false;

    [SerializeField] private float hangTime = 0.2f;
    private float hangCounter;

    [Header("Double Jump Variables")]
    [SerializeField] private GameObject doubleJumpHalo = null;
    [SerializeField] private Transform doubleJumpHaloPoint = null;
    [SerializeField] private bool enableDoubleJump = false;

    [SerializeField] private float doubleJumpForce = 0.0f;

    [Header("State Booleans")]
    public bool isWalking = false;
    public bool isJogging = false;
    public bool isRunning = false;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private bool ceilingHit = false;

    [Header("Slope Handling Variables")]
    public bool isGrounded = false;
    [SerializeField] private bool isOnSlope = false;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] private float slopeFriction = 0.0f;
    [SerializeField] private float rayLength = 0.0f;
    [SerializeField] private float unknownValue = 0.4f;

    public CapsuleCollider2D capCollider;
    public BoxCollider2D boxCollider;


    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        anim = GetComponent<Animator>();
        pMovementControls = GetComponent<PlayerMovementControls>();
        gamepadTest = new GamepadTest();
    }

    // Start is called before the first frame update
    void Start()
    {
        time = dashTimer;
        canDash = true;
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d = GetComponent<Rigidbody2D>();


        #region X Movement & Slope
        if (pMovementControls.move.x > 0.0f && pMovementControls.move.x < walkTilt || pMovementControls.move.x < 0.0f && pMovementControls.move.x >= -walkTilt)
        {
            isWalking = true;
            isJogging = false;
            isRunning = false;
        }
        else if (pMovementControls.move.x == 0.0f)
        {
            isWalking = false;
        }

        if (pMovementControls.move.x >= jogTilt || pMovementControls.move.x <= -jogTilt)
        {
            isWalking = false;
            isJogging = true;
            isRunning = false;
        }
        else if (pMovementControls.move.x == 0.0f)
        {
            isJogging = false;
        }

        if (pMovementControls.move.x >= runTilt || pMovementControls.move.x <= -runTilt)
        {
            isWalking = false;
            isJogging = false;
            isRunning = true;
        }
        else if (pMovementControls.move.x == 0.0f)
        {
            isRunning = false;
        }
        #endregion

        #region Jumping

        // Coyote Time Jumping
        if (!justJumped && pMovementControls.jumpValue >= 1.0f && hangCounter > 0)
        {
            isJumping = true;
            justJumped = true;
            extraJumps--;
            //jumpBufferCount = 0.0f;
        }

        // Jump Off the Ground
        if (isGrounded && !justJumped && pMovementControls.jumpValue >= 1.0f)
        {
            isJumping = true;
            justJumped = true;
            extraJumps--;
        }
        
        // Released Jump Button After Landing
        if (isGrounded && justJumped && pMovementControls.jumpValue <= 0.0f)
        {
            isJumping = false;
            justJumped = false;
        }

        // Letting Go of Jump Button
        if (pMovementControls.jumpValue <= 0.0f)
        {
            isJumping = false;
        }

        // Check When Double Jump Can be Triggered
        if (!isGrounded && pMovementControls.jumpValue <= 0.0f)
        {
            canDoubleJump = true;
        }

        // Do Double Jump
        if (pMovementControls.jumpValue >= 1.0f && extraJumps == 1 && canDoubleJump && !isGrounded && enableDoubleJump)
        {
            activateDoubleJump = true;
            //StartCoroutine(DoubleJump());
        }
        #endregion

        #region Hang Time Counter
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        #endregion

        #region Dashing
        if (pMovementControls.move.x > 0.0f && pMovementControls.dashValue >= 1.0f && canDash)
        {
            isDashing = true;
        }

        if (pMovementControls.move.x == 0.0f && pMovementControls.dashValue >= 1.0f && canDash)
        {
            isDashing = true;
        }

        if (pMovementControls.move.x < 0.0f && pMovementControls.dashValue >= 1.0f && canDash)
        {
            isDashing = true;
        }

        if (!canDash)
        {
            time = dashTimer;

            dashCooldownTime -= Time.deltaTime;

            if (dashCooldownTime <= 0.0f && dashButtonReleased)
            {
                canDash = true;
            }
        }

        if (pMovementControls.dashValue == 0)
        {
            dashButtonReleased = true;
        }
        #endregion
    }

    void FixedUpdate()
    {
        Movement();

        #region Sprite Flipping
        if (facingRight == false && pMovementControls.move.x > 0.0f)
        {
            Flip();
        }
        else if (facingRight == true && pMovementControls.move.x < 0.0f)
        {
            Flip();
        }
        #endregion

        if (isJumping)
        {
            StartCoroutine(Jump());
        }

        if (isDashing)
        {
            StartCoroutine(Dash());
        }

        if (activateDoubleJump)
        {
            StartCoroutine(DoubleJump());
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1.0f;
        transform.localScale = scaler;
    }

    void Movement()
    {
        if (isWalking)
        {
            rb2d.velocity = new Vector2(pMovementControls.move.x * walkSpeed, rb2d.velocity.y);
        }
        if (isJogging)
        {
            rb2d.velocity = new Vector2(pMovementControls.move.x * jogSpeed, rb2d.velocity.y);
        }
        else if (isRunning)
        {
            rb2d.velocity = new Vector2(pMovementControls.move.x * runSpeed, rb2d.velocity.y);
        }

        if (isOnSlope && !isWalking && !isJogging && !isRunning && !isDashing)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (isOnSlope && isWalking || isJogging || isRunning || isDashing)
        {
            NormalizeSlope();
            rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }

    }

    void NormalizeSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, rayLength, whatIsGround);
        Debug.DrawRay(transform.position, new Vector2(0.0f, -rayLength), Color.blue);

        if (hit.collider != null && Mathf.Abs(hit.normal.x) > unknownValue)
        {
            pMovementControls.jumpLengthValue = 0.0f;
            isOnSlope = true;

            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.velocity = new Vector2(body.velocity.x - (hit.normal.x * slopeFriction), body.velocity.y);

            Vector3 pos = transform.position;
            pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1);
            transform.position = pos;
        }

        if (hit.collider != null && Mathf.Abs(hit.normal.x) == 0.0f)
        {
            isOnSlope = false;
        }
    }

    IEnumerator Jump()
    {
        if (isGrounded && isJumping)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, fullJump * jumpForce);
        }

        if (isJumping)
        {
            if (jumpTimeCounter > 0.0f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, fullJump * jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (ceilingHit)
        {
            isJumping = false;
        }

        yield return new WaitForSeconds(0.01f);
    }

    IEnumerator DoubleJump()
    {
        activateDoubleJump = false;
        canDoubleJump = false;
        GameObject dJumpHaloClone = Instantiate(doubleJumpHalo, doubleJumpHaloPoint.transform.position, doubleJumpHalo.transform.rotation);
        rb2d.velocity = new Vector2(rb2d.velocity.x, fullJump * doubleJumpForce);
        yield return new WaitForSeconds(0.01f);

        extraJumps--;
    }

    IEnumerator StopJump()
    {
        if (!isGrounded)
        {
            isJumping = false;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Dash()
    {
        // Dash When Moving Right
        if (pMovementControls.move.x > 0.0f && facingRight)
        {
            rb2d.velocity = new Vector2(1.0f * dashSpeed, 0.0f);
            anim.SetBool("isDashing", true);

            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                isDashing = false;
                canDash = false;
                anim.SetBool("isDashing", false);
                dashButtonReleased = false;
            }
        }
        else if (pMovementControls.move.x == 0.0f && facingRight) // Backflip (Facing Right)
        {
            rb2d.velocity = new Vector2(-1.0f * dashSpeed, 0.0f);
            anim.SetBool("isDashing", true);

            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                isDashing = false;
                rb2d.velocity = Vector2.zero;
                canDash = false;
                anim.SetBool("isDashing", false);
                dashButtonReleased = false;
            }
        }
        else if (pMovementControls.move.x < 0.0f && !facingRight) // Dash When Moving Left
        {
            rb2d.velocity = new Vector2(-1.0f * dashSpeed, 0.0f);
            anim.SetBool("isDashing", true);

            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                isDashing = false;
                rb2d.velocity = Vector2.zero;
                canDash = false;
                anim.SetBool("isDashing", false);
                dashButtonReleased = false;
            }
        }
        else if (pMovementControls.move.x == 0.0f && !facingRight) // Backflip (Facing Left)
        {
            rb2d.velocity = new Vector2(1.0f * dashSpeed, 0.0f);
            anim.SetBool("isDashing", true);

            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                isDashing = false;
                rb2d.velocity = Vector2.zero;
                canDash = false;
                anim.SetBool("isDashing", false);
                dashButtonReleased = false;
            }
        }

        yield return new WaitForSeconds(0.01f);
        dashCooldownTime = dashCooldownTimer;
    }

    #region IsGrounded Checks
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpTimeCounter = jumpTime;
            isGrounded = true;
            isJumping = false;
            extraJumps = 2;
            canDoubleJump = false;
            activateDoubleJump = false;
        }

        if (other.gameObject.CompareTag("Ceiling"))
        {
            ceilingHit = true;
        }

        if (other.gameObject.CompareTag("UnstableGround"))
        {
            boxCollider.enabled = false;
            capCollider.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Ceiling"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (other.gameObject.CompareTag("Ceiling"))
        {
            ceilingHit = false;
        }

        if (other.gameObject.CompareTag("UnstableGround"))
        {
            boxCollider.enabled = true;
            capCollider.enabled = false;
        }
    }
    #endregion

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
