using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public Animator animator;
    GamepadTest gamepadTest;
    PlayerMovementControls pMovementControls;

    public bool attacked = false;
    public bool canAttack = false;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Vector2 attackArea;
    public int attackDamage = 35;

    public float attackRate = 2.0f;
    float nextAttackTime = 0.0f;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        pMovementControls = GetComponent<PlayerMovementControls>();
        gamepadTest = new GamepadTest();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (pMovementControls.meleeValue <= 0.0f)
    //    {
    //        canAttack = true;
    //    }

    //    if (Time.time >= nextAttackTime)
    //    {
    //        if (pMovementControls.meleeValue >= 1.0f && canAttack)
    //        {
    //            StartCoroutine(Attack());
    //            nextAttackTime = Time.time + 1.0f / attackRate;
    //        }
    //    }

        
    //}

    //IEnumerator Attack()
    //{
    //    canAttack = false;
    //    animator.SetTrigger("isAttacking");

    //    Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackArea, 0.0f, enemyLayers);

    //    foreach(Collider2D enemy in hitEnemies)
    //    {
    //        //Debug.Log("We hit " + enemy.name);

    //        enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
    //    }

    //    yield return new WaitForSeconds(0.01f);
    //    //Debug.Log("Attacked!");

    //}

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
