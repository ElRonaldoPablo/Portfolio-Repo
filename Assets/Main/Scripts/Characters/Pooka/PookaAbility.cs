using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PookaAbility : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private PookaAbilityZero gLight = null;
    [SerializeField] private PookaAbilityOne quiescence = null;

    [Space]
    [SerializeField][Range(0.0f, 120.0f)] private float cooldown = 3.0f;

    [Header("Guiding Light")]
    [SerializeField] public bool abilityZeroOn = true;
    [SerializeField] [Range(0.0f, 120.0f)] private float abilityZeroCD = 3.0f;

    [Header("Quiescence")]
    public bool abilityOneOn = false;
    [SerializeField] [Range(0.0f, 120.0f)] private float abilityOneCD = 6.0f;

    #region Hidden Variables
    public bool canLT = true;
    private bool triggerReleased = true;
    #endregion

    void Awake()
    {
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();

        gLight = GetComponentInChildren<PookaAbilityZero>();
        quiescence = GetComponentInChildren<PookaAbilityOne>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldown = abilityZeroCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.ltValue >= 1 && canLT && triggerReleased)
        {
            PookaLTAbility();
            canLT = false;
        }

        #region LT Release Detection
        if (playerControls.ltValue == 0)
        {
            triggerReleased = true;
        }
        else if (playerControls.ltValue >= 1)
        {
            triggerReleased = false;
        }
        #endregion

        CooldownRunner();
    }

    private void PookaLTAbility()
    {
        if (abilityZeroOn)
        {
            StartCoroutine(gLight.GuidingLight());
        }
        else if (abilityOneOn)
        {
            StartCoroutine(quiescence.Quiescence());
        }
    }

    private void CooldownRunner()
    {
        if (!canLT)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0.0f)
            {
                canLT = true;

                if (abilityZeroOn)
                {
                    cooldown = abilityZeroCD;
                    Debug.Log("NOTICE: Illumina Ability Zero Cooldown Reset Confirmed.");
                }
                else if (abilityOneOn)
                {
                    cooldown = abilityOneCD;
                    Debug.Log("NOTICE: Illumina Ability One Cooldown Reset Confirmed.");
                }
            }
        }
    }
}
