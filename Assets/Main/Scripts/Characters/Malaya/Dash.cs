using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    #region Out-of-Inspector Variables
    private PlayerControls playerControls;
    private Player player;
    #endregion

    #region Hidden Boolean Variables
    [HideInInspector] public bool isDashing = false;
    [HideInInspector] public bool canDash = true;
    [HideInInspector] public bool dashButtonReleased = false;
    #endregion

    [Header("Dash Properties")]
    [SerializeField] [Range(0.0f, 100.0f)] private float dashSpeed = 60.0f;
    [Range(0.0f, 120.0f)] public float dashTimer = 0.07f;
    [Range(0.0f, 120.0f)] public float dashCooldownTimer = 0.1f;

    [HideInInspector] public float time = 0.0f;
    [HideInInspector] public float dashCooldownTime = 0.0f;

    [Header("Upgraded Dash Properties")]
    public bool upgradedDash = false;
    [SerializeField] [Range(0.0f, 100.0f)] private float upgradedDashSpeed = 60.0f;
    [Range(0.0f, 120.0f)] public float uDTimer = 0.07f;
    [Range(0.0f, 120.0f)] public float uDashCooldownTimer = 0.1f;

    [Header("Ignore Layer Collisions")]
    [SerializeField] private int playerLayerNumber = 6;
    [SerializeField] private int enemyLayerNumber = 9;

    [HideInInspector] public float uDTime = 0.0f;
    [HideInInspector] public float uDashCooldownTime = 0.0f;

    void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
        player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        time = dashTimer;
        uDTime = uDTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DashFunction()
    {
        // Dash When Moving Right
        if (playerControls.move.x > 0.0f && player._facingRight)
        {
            player._rb.velocity = new Vector2(1.0f * dashSpeed, 0.0f);

            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                isDashing = false;
                player._rb.velocity = Vector2.zero;
                canDash = false;
                dashButtonReleased = false;
            }
        }
        else if (playerControls.move.x == 0.0f && player._facingRight) // Backflip (Facing Right)
        {
            player._rb.velocity = new Vector2(-1.0f * dashSpeed, 0.0f);

            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                isDashing = false;
                player._rb.velocity = Vector2.zero;
                canDash = false;
                dashButtonReleased = false;
            }
        }
        else if (playerControls.move.x < 0.0f && !player._facingRight) // Dash When Moving Left
        {
            player._rb.velocity = new Vector2(-1.0f * dashSpeed, 0.0f);

            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                isDashing = false;
                player._rb.velocity = Vector2.zero;
                canDash = false;
                dashButtonReleased = false;
            }
        }
        else if (playerControls.move.x == 0.0f && !player._facingRight) // Backflip (Facing Left)
        {
            player._rb.velocity = new Vector2(1.0f * dashSpeed, 0.0f);

            time -= Time.deltaTime;

            if (time <= 0.0f)
            {
                isDashing = false;
                player._rb.velocity = Vector2.zero;
                canDash = false;
                dashButtonReleased = false;
            }
        }

        yield return new WaitForSeconds(0.01f);
        dashCooldownTime = dashCooldownTimer;
    }

    public IEnumerator UpgradedDash()
    {
        // Dash When Moving Right
        if (playerControls.move.x > 0.0f && player._facingRight)
        {
            Physics2D.IgnoreLayerCollision(playerLayerNumber, enemyLayerNumber, true);
            player._rb.velocity = new Vector2(1.0f * upgradedDashSpeed, 0.0f);

            uDTime -= Time.deltaTime;

            if (uDTime <= 0.0f)
            {
                isDashing = false;
                player._rb.velocity = Vector2.zero;
                canDash = false;
                dashButtonReleased = false;
            }
        }
        else if (playerControls.move.x == 0.0f && player._facingRight) // Backflip (Facing Right)
        {
            Physics2D.IgnoreLayerCollision(playerLayerNumber, enemyLayerNumber, true);
            player._rb.velocity = new Vector2(-1.0f * upgradedDashSpeed, 0.0f);

            uDTime -= Time.deltaTime;

            if (uDTime <= 0.0f)
            {
                isDashing = false;
                player._rb.velocity = Vector2.zero;
                canDash = false;
                dashButtonReleased = false;
            }
        }
        else if (playerControls.move.x < 0.0f && !player._facingRight) // Dash When Moving Left
        {
            Physics2D.IgnoreLayerCollision(playerLayerNumber, enemyLayerNumber, true);
            player._rb.velocity = new Vector2(-1.0f * upgradedDashSpeed, 0.0f);

            uDTime -= Time.deltaTime;

            if (uDTime <= 0.0f)
            {
                isDashing = false;
                player._rb.velocity = Vector2.zero;
                canDash = false;
                dashButtonReleased = false;
            }
        }
        else if (playerControls.move.x == 0.0f && !player._facingRight) // Backflip (Facing Left)
        {
            Physics2D.IgnoreLayerCollision(playerLayerNumber, enemyLayerNumber, true);
            player._rb.velocity = new Vector2(1.0f * upgradedDashSpeed, 0.0f);

            uDTime -= Time.deltaTime;

            if (uDTime <= 0.0f)
            {
                isDashing = false;
                player._rb.velocity = Vector2.zero;
                canDash = false;
                dashButtonReleased = false;
            }
        }
        yield return new WaitForSeconds(0.01f);
        Physics2D.IgnoreLayerCollision(playerLayerNumber, enemyLayerNumber, false);
        uDashCooldownTime = uDashCooldownTimer;
    }

    public void DashStateDetection()
    {
        if (playerControls.move.x > 0.0f && playerControls.dashValue >= 1.0f && canDash && !player._disableDashing)
        {
            isDashing = true;
        }

        if (playerControls.move.x == 0.0f && playerControls.dashValue >= 1.0f && canDash && !player._disableDashing)
        {
            player._rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            isDashing = true;
        }

        if (playerControls.move.x < 0.0f && playerControls.dashValue >= 1.0f && canDash && !player._disableDashing)
        {
            isDashing = true;
        }

        if (!canDash && !upgradedDash)
        {
            time = dashTimer;
            dashCooldownTime -= Time.deltaTime;

            if (dashCooldownTime <= 0.0f && dashButtonReleased)
            {
                canDash = true;
            }
        }
        else if (!canDash && upgradedDash)
        {
            uDTime = uDTimer;
            uDashCooldownTime -= Time.deltaTime;

            if (uDashCooldownTime <= 0.0f && dashButtonReleased)
            {
                canDash = true;
            }
        }

        if (playerControls.dashValue == 0)
        {
            dashButtonReleased = true;
        }
    }
}
