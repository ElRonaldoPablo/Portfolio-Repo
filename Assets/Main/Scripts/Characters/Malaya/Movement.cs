using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Out-of-Inspector Variables
    private Player player;
    private PlayerControls playerControls;
    private Dash dash;
    #endregion

    [Header("Analog Stick Tilt Values")]
    [Range(-1.0f, 1.0f)] public float walkTilt = 0.55f;
    [Range(-1.0f, 1.0f)] public float jogTilt = 0.75f;
    [Range(-1.0f, 1.0f)] public float runTilt = 0.9f;

    [Header("Movement Speeds")]
    [Range(1.0f, 1000.0f)] public float walkSpeed = 4.0f;
    [Range(1.0f, 1000.0f)] public float jogSpeed = 6.0f;
    [Range(1.0f, 1000.0f)] public float runSpeed = 9.0f;

    [Header("Slope Handling Variables")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float rayLength = 1.3f;
    [SerializeField] private float slopeFriction = 0.1f;
    [SerializeField] private float unknownValue = 0.3f;

    [SerializeField] private float walkValue = 0.55f;
    [SerializeField] private float jogValue = 0.75f;
    [SerializeField] private float runValue = 1.0f;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        player = GetComponent<Player>();
        playerControls = GetComponent<PlayerControls>();
        dash = GetComponent<Dash>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovementFunction()
    {
        if (player._isWalking)
        {
            if (playerControls.move.x > 0.0f)
            {
                player._rb.velocity = new Vector2(walkValue * walkSpeed, player._rb.velocity.y);
            }
            else if (playerControls.move.x < 0.0f)
            {
                player._rb.velocity = new Vector2(-walkValue * walkSpeed, player._rb.velocity.y);
            }
        }
        if (player._isJogging)
        {
            if (playerControls.move.x > 0.0f)
            {
                player._rb.velocity = new Vector2(jogValue * jogSpeed, player._rb.velocity.y);
            }
            else if (playerControls.move.x < 0.0f)
            {
                player._rb.velocity = new Vector2(-jogValue * jogSpeed, player._rb.velocity.y);
            }
        }
        else if (player._isRunning)
        {
            if (playerControls.move.x > 0.0f)
            {
                player._rb.velocity = new Vector2(runValue * runSpeed, player._rb.velocity.y);
            }
            else if (playerControls.move.x < 0.0f)
            {
                player._rb.velocity = new Vector2(-runValue * runSpeed, player._rb.velocity.y);
            }
        }

        if (player._isOnSlope && !player._isWalking && !player._isJogging && !player._isRunning)
        {
            NormalizeSlope();
            player._rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (player._isOnSlope && player._isWalking || player._isOnSlope && player._isJogging || player._isOnSlope && player._isRunning || player._isOnSlope && dash.isDashing)
        {
            NormalizeSlope();
            player._rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void NormalizeSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, rayLength, whatIsGround);
        Debug.DrawRay(transform.position, new Vector2(0.0f, -rayLength), Color.blue);

        if (hit.collider != null && Mathf.Abs(hit.normal.x) > unknownValue)
        {
            playerControls.jumpLengthValue = 0.0f;
            player._isOnSlope = true;

            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.velocity = new Vector2(body.velocity.x - (hit.normal.x * slopeFriction), body.velocity.y);

            Vector3 pos = transform.position;
            pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1);
            transform.position = pos;
        }

        if (hit.collider != null && Mathf.Abs(hit.normal.x) == 0.0f)
        {
            player._isOnSlope = false;
        }
    }

    public void MovementStateDetection()
    {
        if (playerControls.move.x > 0.0f && playerControls.move.x < walkTilt || playerControls.move.x < 0.0f && playerControls.move.x >= -walkTilt)
        {
            player._isWalking = true;
            player._isJogging = false;
            player._isRunning = false;
        }
        else if (playerControls.move.x == 0.0f)
        {
            player._isWalking = false;
        }

        if (playerControls.move.x >= jogTilt || playerControls.move.x <= -jogTilt)
        {
            player._isWalking = false;
            player._isJogging = true;
            player._isRunning = false;
        }
        else if (playerControls.move.x == 0.0f)
        {
            player._isJogging = false;
        }

        if (playerControls.move.x >= runTilt || playerControls.move.x <= -runTilt)
        {
            player._isWalking = false;
            player._isJogging = false;
            player._isRunning = true;
        }
        else if (playerControls.move.x == 0.0f)
        {
            player._isRunning = false;
        }
    }
}
