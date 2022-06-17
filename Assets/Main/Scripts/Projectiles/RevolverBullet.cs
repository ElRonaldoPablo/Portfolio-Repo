using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverBullet : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private Player player = null;

    [SerializeField] private float speed = 1.0f;
    public int damage = 10;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        BulletVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BulletVelocity()
    {
        if (player._facingRight)
        {
            rb.velocity = new Vector2(1.0f * speed, 0.0f);
        }
        else
        {
            rb.velocity = new Vector2(-1.0f * speed, 0.0f);
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
