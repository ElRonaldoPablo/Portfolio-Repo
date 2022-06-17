using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    #region Out-of-Inspector Variables
    private Player player;
    private PlayerControls playerControls;

    private Follow follow;
    #endregion

    [Header("Jump Variables")]
    [SerializeField][Range(0.0f, 50.0f)] private float jumpForce = 16.0f;
    [Range(0.0f, 50.0f)] public float jumpTime = 0.25f;
    [Range(0, 3)] public int extraJumps = 2;
    [HideInInspector] public float jumpTimeCounter;
    [HideInInspector] public float fullJump = 1.0f;

    [Header("Double Jump Variables")]
    public bool enableDoubleJump = false;
    [SerializeField] [Range(0.0f, 100.0f)] private float doubleJumpForce = 30.0f;

    [Space]
    [SerializeField] private GameObject doubleJumpHalo = null;
    [SerializeField] private Transform doubleJumpHaloPoint = null;
    [HideInInspector] public bool canDoubleJump = false;
    [HideInInspector] public bool activateDoubleJump = false;

    [Header("Coyote Time")]
    [Range(0.0f, 2.0f)] public float hangTime = 0.06f;
    [HideInInspector] public float hangCounter;

    #region Hidden Boolean variables
    /*[HideInInspector]*/ public bool isGrounded = false;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool justJumped = false;
    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        //follow = GameObject.FindGameObjectWithTag("Pooka").GetComponent<Follow>();
        player = GetComponent<Player>();
        playerControls = GetComponent<PlayerControls>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HangTimeCounter();
    }

    public IEnumerator JumpFunction()
    {
        if (/*player.*/isGrounded && /*player.*/isJumping)
        {
            player._rb.velocity = new Vector2(player._rb.velocity.x, fullJump * jumpForce);
        }

        if (/*player.*/isJumping)
        {
            if (jumpTimeCounter > 0.0f)
            {
                player._rb.velocity = new Vector2(player._rb.velocity.x, fullJump * jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                /*player.*/isJumping = false;
            }
        }

        if (player._ceilingHit)
        {
            /*player.*/isJumping = false;
        }

        yield return new WaitForSeconds(0.01f);
    }

    public IEnumerator DoubleJump()
    {
        activateDoubleJump = false;
        canDoubleJump = false;
        GameObject dJumpHaloClone = Instantiate(doubleJumpHalo, doubleJumpHaloPoint.transform.position, doubleJumpHalo.transform.rotation);
        player._rb.velocity = new Vector2(player._rb.velocity.x, fullJump * doubleJumpForce);
        yield return new WaitForSeconds(0.01f);

        extraJumps--;
    }

    public void JumpStateDetection()
    {
        // Coyote Time Jumping
        if (!justJumped && playerControls.jumpValue >= 1.0f && hangCounter > 0 && !isGrounded)
        {
            isJumping = true;
            justJumped = true;
            extraJumps--;
            //jumpBufferCount = 0.0f;
        }

        // Jump Off the Ground
        if (/*player.*/isGrounded && !/*player.*/justJumped && playerControls.jumpValue >= 1.0f && !player._disableJumping)
        {
            /*player.*/isJumping = true;
            /*player.*/justJumped = true;
            extraJumps--;
        }

        // Released Jump Button After Landing
        if (/*player.*/isGrounded && /*player.*/justJumped && playerControls.jumpValue <= 0.0f)
        {
            /*player.*/isJumping = false;
            /*player.*/justJumped = false;
        }

        // Letting Go of Jump Button
        if (playerControls.jumpValue <= 0.0f)
        {
            /*player.*/isJumping = false;
        }

        // Check When Double Jump Can be Triggered
        if (!/*player.*/isGrounded && playerControls.jumpValue <= 0.0f && extraJumps == 1)
        {
            canDoubleJump = true;
        }

        // Do Double Jump
        if (playerControls.jumpValue >= 1.0f && extraJumps == 1 && canDoubleJump && !/*player.*/isGrounded && enableDoubleJump)
        {
            activateDoubleJump = true;
        }
    }

    private void HangTimeCounter()
    {
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Slope"))
        {
            jumpTimeCounter = jumpTime;
            isGrounded = true;
            isJumping = false;
            extraJumps = 2;
            canDoubleJump = false;
            activateDoubleJump = false;
            justJumped = true;

            //follow.followDistance = 1.5f;
        }

        if (other.gameObject.CompareTag("Jump1"))
        {
            jumpTimeCounter = jumpTime;
            isGrounded = true;
            isJumping = false;
            extraJumps = 2;
            canDoubleJump = false;
            activateDoubleJump = false;
            justJumped = true;
        }

        if (other.gameObject.CompareTag("Jump2"))
        {
            jumpTimeCounter = jumpTime;
            isGrounded = true;
            isJumping = false;
            extraJumps = 2;
            canDoubleJump = false;
            activateDoubleJump = false;
            justJumped = true;
        }

        if (other.gameObject.CompareTag("Jump3"))
        {
            jumpTimeCounter = jumpTime;
            isGrounded = true;
            isJumping = false;
            extraJumps = 2;
            canDoubleJump = false;
            activateDoubleJump = false;
            justJumped = true;
        }

        if (other.gameObject.CompareTag("Jump4"))
        {
            jumpTimeCounter = jumpTime;
            isGrounded = true;
            isJumping = false;
            extraJumps = 2;
            canDoubleJump = false;
            activateDoubleJump = false;
            justJumped = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Slope"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Jump1"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Jump2"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Jump3"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Jump4"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Slope"))
        {
            isGrounded = false;
        }

        if (other.gameObject.CompareTag("Jump1"))
        {
            isGrounded = false;
        }

        if (other.gameObject.CompareTag("Jump2"))
        {
            isGrounded = false;
        }

        if (other.gameObject.CompareTag("Jump3"))
        {
            isGrounded = false;
        }

        if (other.gameObject.CompareTag("Jump4"))
        {
            isGrounded = false;
        }
    }
}
