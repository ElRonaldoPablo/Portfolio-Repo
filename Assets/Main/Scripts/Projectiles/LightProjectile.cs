using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProjectile : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private Pooka pooka = null;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Vector2 velocity;
    public int damage = 10;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        pooka = GameObject.FindGameObjectWithTag("Pooka").GetComponent<Pooka>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //BulletVelocity();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        BulletVelocity();
    }

    private void BulletVelocity()
    {
        if (pooka.facingRight)
        {
            //rb.velocity = new Vector2(1.0f * speed, 0.0f);
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        else
        {
            //rb.velocity = new Vector2(-1.0f * speed, 0.0f);
            rb.MovePosition(rb.position + -velocity * Time.fixedDeltaTime);
        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
