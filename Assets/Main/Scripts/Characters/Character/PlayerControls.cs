using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    #region Out-of-Inspector Variables

    ProjectIlluminaIA piInputActions;

    [HideInInspector] public float jumpValue;
    [HideInInspector] public float jumpLengthValue;
    [HideInInspector] public float dashValue;
    [HideInInspector] public float healValue;
    [HideInInspector] public float meleeValue;
    [HideInInspector] public float buttonNorthValue;

    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 look;

    [HideInInspector] public float rtValue;
    [HideInInspector] public float dpadRightValue;

    [HideInInspector] public float ltValue;
    [HideInInspector] public float dpadLeftValue;

    [HideInInspector] public float interactValue;

    [HideInInspector] public float leftStickPressValue;
    [HideInInspector] public float rightStickPressValue;

    #endregion

    private void Awake()
    {
        piInputActions = new ProjectIlluminaIA();
    }

    // Start is called before the first frame update
    void Start()
    {
        #region Horizontal Movement
        piInputActions.Player.Move.performed += context => move = context.ReadValue<Vector2>();
        piInputActions.Player.Move.canceled += context => move = Vector2.zero;
        #endregion
        #region Dash
        piInputActions.Player.Dash.performed += context => dashValue = context.ReadValue<float>();
        piInputActions.Player.Dash.canceled += context => dashValue = 0.0f;
        #endregion
        #region Heal
        piInputActions.Player.Heal.performed += context => healValue = context.ReadValue<float>();
        piInputActions.Player.Heal.canceled += context => healValue = 0.0f;
        #endregion

        #region Jump
        piInputActions.Player.Jump.performed += context => jumpValue = context.ReadValue<float>();
        piInputActions.Player.Jump.canceled += context => jumpValue = 0.0f;
        #endregion

        #region Melee Attack

        piInputActions.Player.MeleeAttack.performed += context => meleeValue = context.ReadValue<float>();
        piInputActions.Player.MeleeAttack.canceled += context => meleeValue = 0.0f;

        #endregion
        #region Key Item/Environmental Ability
        piInputActions.Player.ButtonNorth.performed += context => buttonNorthValue = context.ReadValue<float>();
        piInputActions.Player.ButtonNorth.canceled += context => buttonNorthValue = 0.0f;
        #endregion

        #region Look Around
        piInputActions.Player.LookAround.performed += context => look = context.ReadValue<Vector2>();
        piInputActions.Player.LookAround.canceled += context => look = Vector2.zero;
        #endregion

        #region Malaya RT Ability
        piInputActions.Player.MalayaAbility.performed += context => rtValue = context.ReadValue<float>();
        piInputActions.Player.MalayaAbility.canceled += context => rtValue = 0.0f;
        #endregion
        #region DPad Right
        piInputActions.Player.SelectMalayaAbility.performed += context => dpadRightValue = context.ReadValue<float>();
        piInputActions.Player.SelectMalayaAbility.canceled += context => dpadRightValue = 0.0f;
        #endregion

        #region Pooka LT Ability
        piInputActions.Player.PookaAbility.performed += context => ltValue = context.ReadValue<float>();
        piInputActions.Player.PookaAbility.canceled += context => ltValue = 0.0f;
        #endregion
        #region DPad Left
        piInputActions.Player.SelectPookaAbility.performed += context => dpadLeftValue = context.ReadValue<float>();
        piInputActions.Player.SelectPookaAbility.canceled += context => dpadLeftValue = 0.0f;
        #endregion

        #region Interact
        piInputActions.Player.Interact.performed += context => interactValue = context.ReadValue<float>();
        piInputActions.Player.Interact.canceled += context => interactValue = 0.0f;
        #endregion

        #region Left/Right Stick Press
        piInputActions.Player.LeftStickPress.performed += context => leftStickPressValue = context.ReadValue<float>();
        piInputActions.Player.LeftStickPress.canceled += context => leftStickPressValue = 0.0f;

        piInputActions.Player.RightStickPress.performed += context => rightStickPressValue = context.ReadValue<float>();
        piInputActions.Player.RightStickPress.canceled += context => rightStickPressValue = 0.0f;
        #endregion
    }

    #region OnEnable/OnDisable Functions
    private void OnEnable()
    {
        piInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        piInputActions.Player.Disable();
    }
    #endregion
}
