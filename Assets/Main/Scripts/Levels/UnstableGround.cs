using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableGround : MonoBehaviour
{
    #region Out-of-Inspector Variables

    SpriteRenderer sprRenderer;

    #endregion

    [SerializeField] private Rigidbody2D rb2d = null;
    [SerializeField] private GameObject player = null;

    [Space]
    [SerializeField][Range(0.0f, 10.0f)] private float collapseDelay = 0.5f;

    [Space]
    [SerializeField] private bool steppedOn = false;

    void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        steppedOn = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<Player>()._isWalking && steppedOn)
        {
            sprRenderer.color = Color.blue;
        }

        if (player.GetComponent<Player>()._isJogging && steppedOn)
        {
            StartCoroutine(UnstableGroundFunction());
        }

        if (player.GetComponent<Player>()._isRunning && steppedOn)
        {
            StartCoroutine(UnstableGroundFunction());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PitTrigger")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            steppedOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            steppedOn = false;
        }
    }

    IEnumerator UnstableGroundFunction()
    {
        sprRenderer.color = Color.red;
        yield return new WaitForSeconds(collapseDelay);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

}
