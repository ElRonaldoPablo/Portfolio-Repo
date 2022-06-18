using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemUse : MonoBehaviour
{
    private PlayerControls playerControls;
    private bool canUseKeyItem = true;
    [SerializeField] private float timer;
    [SerializeField] private float cooldown = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Use Key Item
        if (canUseKeyItem && playerControls.buttonNorthValue == 1.0f)
        {
            UseKeyItem();
        }
        #endregion

        if (!canUseKeyItem)
        {
            KeyItemUseCooldown();
        }
    }

    private void UseKeyItem()
    {
        Debug.LogWarning("NOTICE: Key Item has been used.");
        canUseKeyItem = false;
        timer = cooldown;
    }

    private void KeyItemUseCooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            canUseKeyItem = true;
        }
    }

}
