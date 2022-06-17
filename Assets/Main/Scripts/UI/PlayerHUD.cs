using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[HelpURL("https://youtu.be/F_xNNzS7st0")]
public class PlayerHUD : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Image hpBar = null;

    [SerializeField][Range(0.0f, 9999.9f)] private float currentHealth = 0f;
    [SerializeField] [Range(0.0f, 9999.9f)] private float maxHealth = 100f;

    [Header("LB/RB Abilities")]
    [SerializeField] private GameObject dashIcon = null;
    [SerializeField] private GameObject healIcon = null;

    [Header("LT/RT Abilities")]
    [SerializeField] private GameObject rtZeroIcon = null;
    [SerializeField] private GameObject rtOneIcon = null;

    [SerializeField] private GameObject ltZeroIcon = null;
    [SerializeField] private GameObject ltOneIcon = null;

    #region Out-of-Inspector Variables
    private Player player = null;
    private Dash dash = null;
    private Heal heal = null;
    private MalayaAbility malayaAbility = null;
    private PookaAbility pookaAbility = null;
    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dash = GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>();
        //heal = GameObject.FindGameObjectWithTag("Pooka").GetComponent<Heal>();
        malayaAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<MalayaAbility>();
        //pookaAbility = GameObject.FindGameObjectWithTag("Pooka").GetComponent<PookaAbility>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar();
        DashIcon();
        //HealIcon();
        RTIcon();
        //LTIcon();
    }

    private void HealthBar()
    {
        currentHealth = player._currentHP;
        maxHealth = player._maxHP;
        float healthPercentage = currentHealth / maxHealth;
        hpBar.fillAmount = healthPercentage;

        if (currentHealth >=  50f)
        {
            hpBar.color = new Color(0.3137f, 0.6313f, 0.3176f, 1f);
        }
        else if (currentHealth >= 25f && currentHealth <=49f)
        {
            hpBar.color = new Color(1f, 0.7254f, 0f, 1f);
        }
        else if (currentHealth >= 0f && currentHealth <= 24f)
        {
            hpBar.color = new Color(1f, 0.3333f, 0.2745f, 1f);
        }
    }

    private void DashIcon()
    {
        if (dash.canDash)
        {
            dashIcon.SetActive(true);
        }
        else
        {
            dashIcon.SetActive(false);
        }
    }

    //private void HealIcon()
    //{
    //    if (heal.canHeal)
    //    {
    //        healIcon.SetActive(true);
    //    }
    //    else
    //    {
    //        healIcon.SetActive(false);
    //    }
    //}

    private void RTIcon()
    {
        #region RT Ability Zero On
        if (malayaAbility.abilityZeroOn && malayaAbility.canRT)
        {
            rtOneIcon.SetActive(false);
            rtZeroIcon.SetActive(true);
        }
        else if (malayaAbility.abilityZeroOn && !malayaAbility.canRT)
        {
            rtZeroIcon.SetActive(false);
            rtOneIcon.SetActive(false);
        }
        #endregion
        #region RT Ability One On
        if (malayaAbility.abilityOneOn && malayaAbility.canRT)
        {
            rtZeroIcon.SetActive(false);
            rtOneIcon.SetActive(true);
        }
        else if (malayaAbility.abilityOneOn && !malayaAbility.canRT)
        {
            rtZeroIcon.SetActive(false);
            rtOneIcon.SetActive(false);
        }
        #endregion
    }

    //private void LTIcon()
    //{
    //    #region LT Ability Zero On
    //    if (pookaAbility.abilityZeroOn && pookaAbility.canLT)
    //    {
    //        ltOneIcon.SetActive(false);
    //        ltZeroIcon.SetActive(true);
    //    }
    //    else if (pookaAbility.abilityZeroOn && !pookaAbility.canLT)
    //    {
    //        ltZeroIcon.SetActive(false);
    //        ltOneIcon.SetActive(false);
    //    }
    //    #endregion
    //    #region LT Ability One On
    //    if (pookaAbility.abilityOneOn && pookaAbility.canLT)
    //    {
    //        ltZeroIcon.SetActive(false);
    //        ltOneIcon.SetActive(true);
    //    }
    //    else if (pookaAbility.abilityOneOn && !pookaAbility.canLT)
    //    {
    //        ltZeroIcon.SetActive(false);
    //        ltOneIcon.SetActive(false);
    //    }
    //    #endregion
    //}
}
