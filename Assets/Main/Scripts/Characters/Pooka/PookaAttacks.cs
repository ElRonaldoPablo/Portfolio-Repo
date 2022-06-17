using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaAttacks : MonoBehaviour
{
    #region Out-of-Inspector Variables
    private PlayerControls playerControls;

    private bool canAttack = true;
    private bool pookaAttackButtonReleased = true;
    private int selectedAttack;
    #endregion

    [SerializeField] private PookaAttackZero taunt = null;
    [SerializeField] private PookaAttackOne strike = null;
    [SerializeField] private PookaAttackTwo projectile = null;

    [Space]
    [SerializeField] [Range(0.0f, 120.0f)] private float cooldown = 3.0f;

    [Header("Taunt")]
    [SerializeField] public bool attackZeroOn = true;
    [SerializeField] [Range(0.0f, 120.0f)] private float attackZeroCD = 3.0f;

    [Header("Strike")]
    public bool attackOneOn = true;
    [SerializeField] [Range(0.0f, 120.0f)] private float attackOneCD = 6.0f;

    [Header("Projectile")]
    public bool attackTwoOn = true;
    [SerializeField] [Range(0.0f, 120.0f)] private float attackTwoCD = 12.0f;

    //public bool isAttacking = false;

    void Awake()
    {
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();

        taunt = GetComponentInChildren<PookaAttackZero>();
        strike = GetComponentInChildren<PookaAttackOne>();
        projectile = GetComponentInChildren<PookaAttackTwo>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.buttonNorthValue >= 1 && canAttack && pookaAttackButtonReleased)
        {
            PookaAttack();
            canAttack = false;
        }

        #region Pooka Attack Button Release Detection
        if (playerControls.buttonNorthValue == 0)
        {
            pookaAttackButtonReleased = true;
        }
        else if (playerControls.buttonNorthValue >= 1)
        {
            pookaAttackButtonReleased = false;
        }
        #endregion

        AttackCooldown();
    }

    private void PookaAttack()
    {
        #region Select Pooka Attack
        selectedAttack = Random.Range(0, 3);

        if (selectedAttack == 0 && attackZeroOn)
        {
            attackZeroOn = false;
            cooldown = attackZeroCD;
        }

        if (selectedAttack == 1 && attackOneOn)
        {
            attackOneOn = false;
            cooldown = attackOneCD;
        }

        if (selectedAttack == 2 && attackTwoOn)
        {
            attackTwoOn = false;
            cooldown = attackTwoCD;
        }
        #endregion
        #region Execute Selected Attack
        if (!attackZeroOn)
        {
            StartCoroutine(taunt.Taunt());
        }
        
        if (!attackOneOn)
        {
            //StartCoroutine(strike.Strike());
            strike.isStriking = true;
        }

        if (!attackTwoOn)
        {
            StartCoroutine(projectile.Projectile());
        }
        #endregion
    }

    private void AttackCooldown()
    {
        if (!canAttack)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0.0f)
            {
                canAttack = true;

                if (!attackZeroOn)
                {
                    attackZeroOn = true;
                }
                else if (!attackOneOn)
                {
                    attackOneOn = true;
                }
                else if (!attackTwoOn)
                {
                    attackTwoOn = true;
                }
            }
        }
    }
}
