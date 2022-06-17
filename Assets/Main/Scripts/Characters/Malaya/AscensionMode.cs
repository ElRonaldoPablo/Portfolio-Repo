using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensionMode : MonoBehaviour
{
    private bool canTransform = true;
    private bool isTransformed = false;
    private PlayerControls playerControls;
    private Heal heal;
    private KeyItemUse keyItemUse;

    private float timer;
    private float cooldown = 2.0f;

    private Animator anim;
    [SerializeField] private RuntimeAnimatorController humanAnim;
    [SerializeField] private RuntimeAnimatorController transformedAnim;

    private Jump jump;
    private Dash dash;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        playerControls = GetComponent<PlayerControls>();
        heal = GetComponent<Heal>();
        keyItemUse = GetComponent<KeyItemUse>();

        jump = GetComponent<Jump>();
        dash = GetComponent<Dash>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Henshin
        if (canTransform && playerControls.leftStickPressValue == 1.0f && playerControls.rightStickPressValue == 1.0f)
        {
            Henshin();
        }
        #endregion

        HenshinCooldown();
    }

    private void Henshin()
    {
        if (!isTransformed)
        {
            #region Ascension Mode

            heal.upgradedHeal = true;
            keyItemUse.enabled = false;

            Debug.LogWarning("NOTICE: Henshin! For the Light! Go forth, Shadowbreaker!");
            isTransformed = true;
            canTransform = false;

            timer = cooldown;

            anim.runtimeAnimatorController = transformedAnim;
            jump.enableDoubleJump = true;
            dash.upgradedDash = true;

            #endregion
        }
        else
        {
            #region Reversion

            heal.upgradedHeal = false;
            keyItemUse.enabled = true;

            Debug.LogWarning("NOTICE: Reverted back to human form.");
            isTransformed = false;
            canTransform = false;

            timer = cooldown;

            anim.runtimeAnimatorController = humanAnim;
            jump.enableDoubleJump = false;
            dash.upgradedDash = false;

            #endregion
        }
    }

    private void HenshinCooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            canTransform = true;
        }
    }
}
