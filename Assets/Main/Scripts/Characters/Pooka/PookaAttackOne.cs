using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaAttackOne : MonoBehaviour
{
    private Rigidbody2D rb = null;

    [SerializeField] private Follow follow = null;
    [SerializeField] private Player player = null;

    private GameObject target = null;
    public bool isStriking = false;
    public bool pauseMovement = false;

    [Range(0, 9999)] public int attackDamage = 20;
    [SerializeField] private Transform attackPoint = null;
    [SerializeField] private Transform actualAttackPoint = null;
    [SerializeField] private Vector2 attackArea = new Vector2(3.0f, 1.5f);

    [Space][SerializeField] private LayerMask enemyLayers;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();

        follow = GetComponentInParent<Follow>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        attackPoint = GameObject.Find("JumpDetector").transform;
        actualAttackPoint = GameObject.Find("AttackPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        target = player._closest;
    }

    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        if (isStriking && target != null)
        {
            StartCoroutine(Strike());
        }
    }

    public IEnumerator Strike()
    {
        print("Strike");
        pauseMovement = true;

        #region Approach Target from the Left
        if (transform.position.x < target.transform.position.x)
        {
            rb.velocity = new Vector2(1.0f * follow.runSpeed, rb.velocity.y);

            if (attackPoint.transform.position.x == target.transform.position.x - 2.0f || attackPoint.transform.position.x >= target.transform.position.x - 2.0f)
            {
                rb.velocity = new Vector2(0.0f, rb.velocity.y);
                
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(actualAttackPoint.position, attackArea, 0.0f, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }

                isStriking = false;

                if (!isStriking)
                {
                    yield return new WaitForSeconds(1.0f);
                    pauseMovement = false;
                }
            }
        }
        #endregion
        #region Approach Target from the Right
        if (transform.position.x > target.transform.position.x)
        {
            rb.velocity = new Vector2(-1.0f * follow.runSpeed, rb.velocity.y);

            if (attackPoint.transform.position.x == target.transform.position.x + 2.0f || attackPoint.transform.position.x <= target.transform.position.x + 2.0f)
            {
                rb.velocity = new Vector2(0.0f, rb.velocity.y);

                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(actualAttackPoint.position, attackArea, 0.0f, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }

                isStriking = false;

                if (!isStriking)
                {
                    yield return new WaitForSeconds(1.0f);
                    pauseMovement = false;
                }
            }
        }
        #endregion
    }

    private void OnDrawGizmos()
    {
        if (actualAttackPoint == null)
        {
            Debug.LogWarning("NOTICE: Attack Point reference is missing.");
            return;
        }

        Gizmos.DrawWireCube(actualAttackPoint.position, attackArea);
        Gizmos.color = Color.yellow;
    }
}
