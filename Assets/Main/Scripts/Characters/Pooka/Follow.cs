using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Player player;
    public GameObject playerObject = null;

    [Header("Speed Variables")]
    [Range(0.0f, 60.0f)] public float walkSpeed = 4.0f;
    [Range(0.0f, 60.0f)] public float jogSpeed = 6.0f;
    [Range(0.0f, 60.0f)] public float runSpeed = 10.0f;

    [SerializeField] [Range(0.0f, 60.0f)] private float farRunSpeed = 12.0f;

    public bool besidePlayer = false;
    [HideInInspector] public float distToPlayerX = 0.0f;

    [Header("Follow Properties")]
    [Range(0.0f, 60.0f)] public float followDistance = 1.5f;

    //public bool fastRun = false;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectDistance();
    }

    public IEnumerator FollowTarget()
    {
        #region Move with the Player
        // To the Right
        if (player._isWalking && distToPlayerX >= followDistance && distToPlayerX <= followDistance + 1.0f && transform.position.x < playerObject.transform.position.x)
        {
            rb.velocity = new Vector2(0.55f * walkSpeed, rb.velocity.y);
        }
        if (player._isJogging && distToPlayerX >= followDistance && distToPlayerX <= followDistance + 1.0f && transform.position.x < playerObject.transform.position.x)
        {
            rb.velocity = new Vector2(0.75f * jogSpeed, rb.velocity.y);
        }
        if (player._isRunning && distToPlayerX >= followDistance && distToPlayerX <= followDistance + 1.0f && transform.position.x < playerObject.transform.position.x)
        {
            rb.velocity = new Vector2(0.9f * runSpeed, rb.velocity.y);
        }

        // To the Left
        if (player._isWalking && distToPlayerX >= followDistance && distToPlayerX <= followDistance + 1.0f && transform.position.x > playerObject.transform.position.x)
        {
            rb.velocity = new Vector2(0.55f * -walkSpeed, rb.velocity.y);
        }
        if (player._isJogging && distToPlayerX >= followDistance && distToPlayerX <= followDistance + 1.0f && transform.position.x > playerObject.transform.position.x)
        {
            rb.velocity = new Vector2(0.75f * -jogSpeed, rb.velocity.y);
        }
        if (player._isRunning && distToPlayerX >= followDistance && distToPlayerX <= followDistance + 1.0f && transform.position.x > playerObject.transform.position.x)
        {
            rb.velocity = new Vector2(0.9f * -runSpeed, rb.velocity.y);
        }
        #endregion

        yield return new WaitForSeconds(0.01f);

        if (!besidePlayer)
        {
            yield return new WaitForSeconds(0.01f);

            #region Run Towards Player

            if (distToPlayerX > followDistance + 2.0f)
            {
                if (transform.position.x < playerObject.transform.position.x)
                {
                    rb.velocity = new Vector2(1.0f * farRunSpeed, rb.velocity.y);
                }

                if (transform.position.x > playerObject.transform.position.x)
                {
                    rb.velocity = new Vector2(1.0f * -farRunSpeed, rb.velocity.y);
                }
            }
            if (distToPlayerX < followDistance + 2.0f && distToPlayerX > followDistance)
            {
                if (transform.position.x < playerObject.transform.position.x)
                {
                    rb.velocity = new Vector2(1.0f * walkSpeed, rb.velocity.y);
                }

                if (transform.position.x > playerObject.transform.position.x)
                {
                    rb.velocity = new Vector2(1.0f * -walkSpeed, rb.velocity.y);
                }
            }
            else if (distToPlayerX <= followDistance)
            {
                rb.velocity = new Vector2(0.0f, rb.velocity.y);
            }
            #endregion
        }
    }

    private void DetectDistance()
    {
        #region X-Axis Distance Detection

        // Player/Pooka Distance Calculation
        distToPlayerX = Vector2.Distance(new Vector2(transform.position.x, playerObject.transform.position.y), playerObject.transform.position);

        if (transform.position.x < playerObject.transform.position.x - followDistance || transform.position.x > playerObject.transform.position.x + followDistance)
        {
            besidePlayer = false;
        }

        if (transform.position.x == playerObject.transform.position.x - followDistance || transform.position.x == playerObject.transform.position.x + followDistance)
        {
            besidePlayer = true;
        }
        #endregion
    }
}
