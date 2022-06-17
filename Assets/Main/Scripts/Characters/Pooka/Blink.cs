using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    #region Hidden Variables
    private Rigidbody2D rb;
    private GameObject playerObject = null;
    private Follow follow;
    #endregion

    [Header("Blink Attributes")]
    [SerializeField][Range(0.0f, 100.0f)] private float xAxisBlinkDist = 14.0f;
    [SerializeField][Range(0.0f, 60.0f)] private float followDistanceY = 5.0f;

    #region Out-of-Inspector Variables
    private Vector2 blinkPos;
    private float distToPlayerY = 0.0f;
    [HideInInspector] public bool canBlink = true;
    [HideInInspector] public bool onSameGroundWithPlayer = false;
    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        follow = GetComponent<Follow>();
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectBlinkDistance();
    }

    public IEnumerator BlinkToTarget()
    {
        #region Vertical Blinking
        if (playerObject.GetComponent<Jump>().isGrounded && !onSameGroundWithPlayer)
        {
            yield return new WaitForSeconds(0.5f);
            blinkPos = playerObject.transform.position;

            if (!onSameGroundWithPlayer && playerObject.GetComponent<Jump>().isGrounded)
            {
                transform.position = new Vector2(blinkPos.x, blinkPos.y + 1.5f);
                rb.constraints = RigidbodyConstraints2D.FreezePosition;

                yield return new WaitForSeconds(0.1f);

                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                canBlink = false;

                yield return new WaitForSeconds(0.01f);

                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                canBlink = true;
            }
            else if (onSameGroundWithPlayer)
            {
                yield return null;
            }
        }
        #endregion
        #region Blink to Player When Off-Screen
        if (playerObject.GetComponent<Jump>().isGrounded && follow.distToPlayerX > xAxisBlinkDist)
        {
            blinkPos = playerObject.transform.position;

            if (canBlink)
            {
                transform.position = new Vector2(blinkPos.x, blinkPos.y + 1.5f);
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                yield return new WaitForSeconds(0.1f);

                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                canBlink = false;
                yield return new WaitForSeconds(0.01f);

                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                canBlink = true;
            }
            else if (onSameGroundWithPlayer)
            {
                yield return null;
            }
        }
        #endregion
    }

    private void DetectBlinkDistance()
    {
        #region Y-Axis Distance Detection
        distToPlayerY = Vector2.Distance(new Vector2(playerObject.transform.position.x, transform.position.y), playerObject.transform.position);

        if (distToPlayerY >= followDistanceY)
        {
            onSameGroundWithPlayer = false;
        }

        if (distToPlayerY <= followDistanceY)
        {
            onSameGroundWithPlayer = true;
        }
        #endregion
    }
}
