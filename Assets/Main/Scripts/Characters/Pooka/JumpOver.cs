using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOver : MonoBehaviour
{
    #region Out-of-Inspector Variables
    private Rigidbody2D rb = null;
    private Follow follow;
    private Pooka pooka;
    private bool jumpHeightOne = false;
    private bool jumpHeightTwo = false;
    private bool jumpHeightThree = false;
    private bool jumpHeightFour = false;
    #endregion

    [Header("Jump Properties")]
    [SerializeField][Range(0.0f, 12.0f)] private float moveSpeed = 0.9f;
    [Space]
    [SerializeField][Range(0.0f, 60.0f)] private float jumpOneForce = 7.0f;
    [SerializeField][Range(0.0f, 60.0f)] private float jumpTwoForce = 7.01f;
    [SerializeField] [Range(0.0f, 60.0f)] private float jumpThreeForce = 7.05f;
    [SerializeField] [Range(0.0f, 60.0f)] private float jumpFourForce = 7.75f;
    public bool isJumping = false;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        follow = GetComponent<Follow>();
        pooka = GetComponent<Pooka>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        if (jumpHeightOne || jumpHeightTwo || jumpHeightThree || jumpHeightFour)
        {
            StartCoroutine(JumpOverGround());
            follow.followDistance = 1.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jump1"))
        {
            jumpHeightOne = true;
            follow.followDistance = 0.0f;
        }

        if (other.gameObject.CompareTag("Jump2"))
        {
            jumpHeightTwo = true;
            follow.followDistance = 0.0f;
        }

        if (other.gameObject.CompareTag("Jump3"))
        {
            jumpHeightThree = true;
            follow.followDistance = 0.0f;
        }

        if (other.gameObject.CompareTag("Jump4"))
        {
            jumpHeightFour = true;
            follow.followDistance = 0.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jump1"))
        {
            jumpHeightOne = false;
        }

        if (other.gameObject.CompareTag("Jump2"))
        {
            jumpHeightTwo = false;
        }

        if (other.gameObject.CompareTag("Jump3"))
        {
            jumpHeightThree = false;
        }

        if (other.gameObject.CompareTag("Jump4"))
        {
            jumpHeightFour = false;
        }
    }

    private IEnumerator JumpOverGround()
    {
        if (jumpHeightOne)
        {
            rb.AddForce(Vector2.up * jumpOneForce, ForceMode2D.Impulse);
            isJumping = true;

            if (pooka.facingRight)
            {
                rb.velocity = new Vector2(1.0f * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-1.0f * moveSpeed, rb.velocity.y);
            }
            
        }

        if (jumpHeightTwo)
        {
            rb.AddForce(Vector2.up * jumpTwoForce, ForceMode2D.Impulse);
            isJumping = true;

            if (pooka.facingRight)
            {
                rb.velocity = new Vector2(1.0f * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-1.0f * moveSpeed, rb.velocity.y);
            }
        }

        if (jumpHeightThree)
        {
            rb.AddForce(Vector2.up * jumpThreeForce, ForceMode2D.Impulse);
            isJumping = true;

            if (pooka.facingRight)
            {
                rb.velocity = new Vector2(1.0f * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-1.0f * moveSpeed, rb.velocity.y);
            }

            yield return new WaitForSeconds(0.2f);
            jumpHeightThree = false;
        }

        if (jumpHeightFour)
        {
            rb.AddForce(Vector2.up * jumpFourForce, ForceMode2D.Impulse);
            isJumping = true;

            if (pooka.facingRight)
            {
                rb.velocity = new Vector2(1.0f * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-1.0f * moveSpeed, rb.velocity.y);
            }

            yield return new WaitForSeconds(0.2f);
            jumpHeightFour = false;
        }

        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}
