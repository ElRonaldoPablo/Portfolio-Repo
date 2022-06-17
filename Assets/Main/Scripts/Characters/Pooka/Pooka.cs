using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooka : Character
{
    private Rigidbody2D rb = null;
    private Follow follow;
    private Blink blink;
    private JumpOver jumpOver;
    private PookaAttackOne pookaStrike;

    public bool facingRight = true;
    public bool faceTarget = false;

    [SerializeField] private Player player = null;
    private GameObject target = null;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        follow = GetComponent<Follow>();
        blink = GetComponent<Blink>();
        jumpOver = GetComponent<JumpOver>();
        pookaStrike = GetComponentInChildren<PookaAttackOne>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target = player._closest;
        Flip();

        //if (facingRight == false && faceTarget && target.transform.position.x > transform.position.x)
        //{
        //    Flip();
        //}
    }

    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        if (!jumpOver.isJumping && !pookaStrike.isStriking && !pookaStrike.pauseMovement)
        {
            StartCoroutine(follow.FollowTarget());
        }

        if (blink.canBlink)
        {
            StartCoroutine(blink.BlinkToTarget());
        }
    }

    public void Flip()
    {
        if (facingRight == false && rb.velocity.x > 0)
        {
            facingRight = !facingRight;
            Vector2 scaler = transform.localScale;
            scaler.x *= -1.0f;
            transform.localScale = scaler;
        }
        else if (facingRight == true && rb.velocity.x < 0)
        {
            facingRight = !facingRight;
            Vector2 scaler = transform.localScale;
            scaler.x *= -1.0f;
            transform.localScale = scaler;
        }

        if (facingRight == false && faceTarget && target.transform.position.x > transform.position.x)
        {
            facingRight = !facingRight;
            Vector2 scaler = transform.localScale;
            scaler.x *= -1.0f;
            transform.localScale = scaler;
        }
        else if (facingRight == true && faceTarget && target.transform.position.x < transform.position.x)
        {
            facingRight = !facingRight;
            Vector2 scaler = transform.localScale;
            scaler.x *= -1.0f;
            transform.localScale = scaler;
        }
    }
}