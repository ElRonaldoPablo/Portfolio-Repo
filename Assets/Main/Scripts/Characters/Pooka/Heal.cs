using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [Header("Referenced Components")]
    private PlayerControls playerControls;
    private Player player;

    [Header("Rejuvenation Properties")]
    [Range(-100, 100)] public int healAmount = 50;
    [Range(-100, 100)] public int advancedHealAmount = 75;

    [Space]
    [Range(0.0f, 300.0f)] public float timer = 0.0f;
    [Range(0.0f, 300.0f)] public float rejuvenationCooldownTimer = 3.0f;
    [Range(0.0f, 300.0f)] public float upgradedRejuvenationTimer = 6.0f;
    public bool canHeal = true;

    public bool upgradedHeal = false;
    public int potionCount = 3;

    #region Out-of-Inspector Variables
    [HideInInspector] public bool isHealing = false;
    private bool pressReleased = true;
    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = rejuvenationCooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        HealStateDetection();

        if (!canHeal)
        {
            HealCooldownTimer();
        }

        if (isHealing && canHeal && !upgradedHeal && potionCount > 0)
        {
            StartCoroutine(player.Rejuvenation());
            potionCount--;
            timer = rejuvenationCooldownTimer;

            Debug.LogWarning("NOTICE: Total number of potions left is " + potionCount + ".");
        }

        if (isHealing && canHeal && upgradedHeal)
        {
            StartCoroutine(player.UpgradedRejuvenation());
            //canHeal = false;
            timer = upgradedRejuvenationTimer;
        }
    }

    public void HealStateDetection()
    {
        if (playerControls.healValue >= 1.0f && pressReleased)
        {
            isHealing = true;
            pressReleased = false;

        }
        else if (playerControls.healValue == 0.0f && !pressReleased)
        {
            isHealing = false;
            pressReleased = true;
        }
    }

    private void HealCooldownTimer()
    {
        timer -= Time.deltaTime;

        if (!upgradedHeal)
        {
            if (timer <= 0.0f)
            {
                canHeal = true;
            }
            else if (timer > 0.0f)
            {
                canHeal = false;
            }

            //canHeal = true;
        }

        if (upgradedHeal)
        {
            if (timer <= 0.0f)
            {
                canHeal = true;
            }
            else if (timer > 0.0f)
            {
                canHeal = false;
            }
            
        }
    }
}
