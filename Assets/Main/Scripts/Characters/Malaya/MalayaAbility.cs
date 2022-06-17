using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalayaAbility : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private MalayaAbilityZero cesario = null;
    [SerializeField] private MalayaAbilityOne ninja = null;

    [Space]
    [SerializeField][Range(0.0f, 120.0f)] private float cooldown = 3.0f;

    [Header("Cesario's Revolver")]
    public bool abilityZeroOn = true;
    [SerializeField][Range(0.0f, 120.0f)] private float abilityZeroCD = 3.0f;

    [Header("NiNja's Shuriken")]
    public bool abilityOneOn = false;
    [SerializeField][Range(0.0f, 120.0f)] private float abilityOneCD = 3.0f;

    #region Hidden Variables
    public bool canRT = true;
    private bool triggerReleased = true;
    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        playerControls = GetComponent<PlayerControls>();

        cesario = GetComponentInChildren<MalayaAbilityZero>();
        ninja = GetComponentInChildren<MalayaAbilityOne>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldown = abilityZeroCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.rtValue >= 1 && canRT && triggerReleased)
        {
            MalayaRTAbility();
            canRT = false;
        }

        #region RT Release Detection
        if (playerControls.rtValue == 0)
        {
            triggerReleased = true;
        }
        else if (playerControls.rtValue >= 1)
        {
            triggerReleased = false;
        }
        #endregion

        CooldownRunner();
    }

    private void MalayaRTAbility()
    {
        if (abilityZeroOn)
        {
            StartCoroutine(cesario.RevolverOfCesario());
            cooldown = abilityZeroCD;
        }
        else if (abilityOneOn)
        {
            StartCoroutine(ninja.ShurikenOfNinja());
            cooldown = abilityOneCD;
        }
    }

    private void CooldownRunner()
    {
        if (!canRT)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0.0f)
            {
                canRT = true;

                //if (abilityZeroOn)
                //{
                //    cooldown = abilityZeroCD;
                //}
                //else if (abilityOneOn)
                //{
                //    cooldown = abilityOneCD;
                //}
            }
        }
    }
}
