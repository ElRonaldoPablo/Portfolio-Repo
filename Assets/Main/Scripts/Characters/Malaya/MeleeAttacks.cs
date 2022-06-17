using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacks : MonoBehaviour
{
    #region Out-of-Inspector Variables
    private PlayerControls playerControls;
    private Animator animator;
    private Player player;

    [HideInInspector] public bool canAttack = false;
    #endregion

    [Header("Melee Attack Range")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackArea = new Vector2(3.0f, 1.5f);

    [Header("Damage & Attributes")]
    [Range(0, 9999)] public int attackDamage = 35;
    [SerializeField][Range(0.0f, 12.0f)] private float attackRate = 2.5f;
    float nextAttackTime = 0.0f;

    [Space][SerializeField] private LayerMask enemyLayers;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        player = GetComponent<Player>();
        playerControls = GetComponent<PlayerControls>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player._stationaryAbility)
        {
            AttackRate();
        }
    }

    private void AttackRate()
    {
        if (playerControls.meleeValue <= 0.0f)
        {
            canAttack = true;
        }

        if (Time.time >= nextAttackTime)
        {
            if (playerControls.meleeValue >= 1.0f && canAttack)
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1.0f / attackRate;
            }
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        animator.SetTrigger("isAttacking");

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackArea, 0.0f, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(0.01f);
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("NOTICE: Attack Point reference is missing.");
            return;
        }

        Gizmos.DrawWireCube(attackPoint.position, attackArea);
        Gizmos.color = Color.yellow;
    }
}
