using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementControls : MonoBehaviour
{
    #region Out-of-Inspector Variables

    GamepadTest gamepadTest;

    #endregion

    public Vector2 move;
    public float jumpValue;
    public float jumpLengthValue;
    public float dashValue;
    public float meleeValue;

    private void Awake()
    {
        gamepadTest = new GamepadTest();
    }

    // Start is called before the first frame update
    void Start()
    {
        #region Horizontal Movement
        gamepadTest.Player.Move.performed += context => move = context.ReadValue<Vector2>();
        gamepadTest.Player.Move.canceled += context => move = Vector2.zero;
        #endregion
        #region Jump
        gamepadTest.Player.Jump.performed += context => jumpValue = context.ReadValue<float>();
        gamepadTest.Player.Jump.canceled += context => jumpValue = 0.0f;
        #endregion
        #region Dash
        gamepadTest.Player.Dash.performed += context => dashValue = context.ReadValue<float>();
        gamepadTest.Player.Dash.canceled += context => dashValue = 0.0f;
        #endregion
        #region Melee Attack

        gamepadTest.Player.MeleeAttack.performed += context => meleeValue = context.ReadValue<float>();
        gamepadTest.Player.MeleeAttack.canceled += context => meleeValue = 0.0f;

        #endregion
    }

    #region Unused Method
    //// Update is called once per frame
    //void Update()
    //{

    //}
    #endregion

    private void OnEnable()
    {
        gamepadTest.Player.Enable();
    }

    private void OnDisable()
    {
        gamepadTest.Player.Disable();
    }
}
