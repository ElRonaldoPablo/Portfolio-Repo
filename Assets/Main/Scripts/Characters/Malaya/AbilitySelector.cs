using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelector : MonoBehaviour
{
    #region Variables
    private PlayerControls playerControls;
    private MalayaAbility malayaAbility;
    private PookaAbility pookaAbility;

    private bool canSelectRT = true;
    private bool rightButtonReleased = true;
    private bool canSelectLT = true;
    private bool leftButtonReleased = true;
    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
        malayaAbility = GetComponent<MalayaAbility>();
        //pookaAbility = GameObject.FindGameObjectWithTag("Pooka").GetComponent<PookaAbility>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region DPad Right
        if (playerControls.dpadRightValue >= 1 && canSelectRT && rightButtonReleased)
        {
            StartCoroutine(MalayaAbilitySelector());
            canSelectRT = false;
        }
        #endregion
        #region DPad Left
        if (playerControls.dpadLeftValue >= 1 && canSelectLT && leftButtonReleased)
        {
            StartCoroutine(PookaAbilitySelector());
            canSelectLT = false;
        }
        #endregion

        #region DPad Right Release Detection
        if (playerControls.dpadRightValue == 0)
        {
            rightButtonReleased = true;
            canSelectRT = true;
        }
        else if (playerControls.rtValue >= 1)
        {
            rightButtonReleased = false;
        }
        #endregion
        #region DPad Left Release Detection
        if (playerControls.dpadLeftValue == 0)
        {
            leftButtonReleased = true;
            canSelectLT = true;
        }
        else if (playerControls.ltValue >= 1)
        {
            leftButtonReleased = false;
        }
        #endregion
    }

    private IEnumerator MalayaAbilitySelector()
    {
        #region Switch from Ability 0 to Ability 1
        if (malayaAbility.abilityZeroOn && canSelectRT)
        {
            malayaAbility.abilityZeroOn = false;
            yield return new WaitForSeconds(0.01f);
            malayaAbility.abilityOneOn = true;
        }
        #endregion

        #region Switch from Ability 1 to Ability 0
        if (malayaAbility.abilityOneOn && canSelectRT)
        {
            malayaAbility.abilityOneOn = false;
            yield return new WaitForSeconds(0.01f);
            malayaAbility.abilityZeroOn = true;
        }
        #endregion
    }

    private IEnumerator PookaAbilitySelector()
    {
        #region Switch from Ability 0 to Ability 1
        if (pookaAbility.abilityZeroOn && canSelectLT)
        {
            pookaAbility.abilityZeroOn = false;
            yield return new WaitForSeconds(0.01f);
            pookaAbility.abilityOneOn = true;
        }
        #endregion

        #region Switch from Ability 1 to Ability 0
        if (pookaAbility.abilityOneOn && canSelectLT)
        {
            pookaAbility.abilityOneOn = false;
            yield return new WaitForSeconds(0.01f);
            pookaAbility.abilityZeroOn = true;
        }
        #endregion
    }
}
