using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/QuickStartGuide.html")]
public class ControlTest : MonoBehaviour
{
    GamepadTest gamepadTest;

    [Header("Buttons")]
    [SerializeField] private GameObject buttonSouthPressed = null;
    [SerializeField] private GameObject buttonNorthPressed = null;
    [SerializeField] private GameObject buttonWestPressed = null;
    [SerializeField] private GameObject buttonEastPressed = null;

    [Header("DPad")]
    [SerializeField] private GameObject dpadDownPressed = null;
    [SerializeField] private GameObject dpadUpPressed = null;
    [SerializeField] private GameObject dpadLeftPressed = null;
    [SerializeField] private GameObject dpadRightPressed = null;

    [Header("Misc")]
    [SerializeField] private GameObject pausePressed = null;
    [SerializeField] private GameObject selectPressed = null;
    [SerializeField] private GameObject touchpadPressed = null;

    [Header("Analog Sticks")]
    [SerializeField] private GameObject leftStickInitPos;
    [SerializeField] private GameObject leftStick = null;
    [Space]
    [SerializeField] private GameObject rightStickInitPos;
    [SerializeField] private GameObject rightStick = null;

    public Vector2 move;
    private Vector2 look;

    [Header("Bumpers")]
    [SerializeField] private GameObject rbPressed = null;
    [SerializeField] private GameObject lbPressed = null;

    [Header("Triggers")]
    [SerializeField] private GameObject rtPressed = null;
    [SerializeField] private GameObject ltPressed = null;



    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        gamepadTest = new GamepadTest();
    }

    // Start is called before the first frame update
    private void Start()
    {
        #region Buttons
        gamepadTest.Player.Jump.performed += context => JumpInputActive();
        gamepadTest.Player.Jump.canceled += context => JumpInputInactive();

        gamepadTest.Player.SecondaryAttack.performed += context => SecondaryAttackInputActive();
        gamepadTest.Player.SecondaryAttack.canceled += context => SecondaryAttackInputInactive();

        gamepadTest.Player.MeleeAttack.performed += context => MeleeAttackInputActive();
        gamepadTest.Player.MeleeAttack.canceled += context => MeleeAttackInputInactive();

        gamepadTest.Player.UseItem.performed += context => UseItemInputActive();
        gamepadTest.Player.UseItem.canceled += context => UseItemInputInactive();
        #endregion
        #region DPad
        gamepadTest.Player.SelectItemDown.performed += context => SelectItemDownInputActive();
        gamepadTest.Player.SelectItemDown.canceled += context => SelectItemDownInputInactive();

        gamepadTest.Player.SelectItemUp.performed += context => SelectItemUpInputActive();
        gamepadTest.Player.SelectItemUp.canceled += context => SelectItemUpInputInactive();

        gamepadTest.Player.SelectIlluminaAbility.performed += context => SelectIlluminaAbilityInputActive();
        gamepadTest.Player.SelectIlluminaAbility.canceled += context => SelectIlluminaAbilityInputInactive();

        gamepadTest.Player.SelectMalayaAbility.performed += context => SelectMalayaAbilityInputActive();
        gamepadTest.Player.SelectMalayaAbility.canceled += context => SelectMalayaAbilityInputInactive();
        #endregion
        #region Misc
        gamepadTest.Player.Pause.performed += context => PauseInputActive();
        gamepadTest.Player.Pause.canceled += context => PauseInputInactive();

        gamepadTest.Player.Select.performed += context => SelectInputActive();
        gamepadTest.Player.Select.canceled += context => SelectInputInactive();

        gamepadTest.Player.Touchpad.performed += context => TouchpadInputActive();
        gamepadTest.Player.Touchpad.canceled += context => TouchpadInputInactive();
        #endregion
        #region Analog Sticks
        gamepadTest.Player.Move.performed += context => move = context.ReadValue<Vector2>();
        gamepadTest.Player.Move.canceled += context => move = Vector2.zero;

        gamepadTest.Player.Look.performed += context => look = context.ReadValue<Vector2>();
        gamepadTest.Player.Look.canceled += context => look = Vector2.zero;
        #endregion
        #region Bumpers
        gamepadTest.Player.Dash.performed += context => DashInputActive();
        gamepadTest.Player.Dash.canceled += context => DashInputInactive();

        gamepadTest.Player.Map.performed += context => MapInputActive();
        gamepadTest.Player.Map.canceled += context => MapInputInactive();
        #endregion
        #region Triggers
        gamepadTest.Player.MalayaAbility.performed += context => MalayaAbilityInputActive();
        gamepadTest.Player.MalayaAbility.canceled += context => MalayaAbilityInputInactive();

        gamepadTest.Player.IlluminaAbility.performed += context => IlluminaAbilityInputActive();
        gamepadTest.Player.IlluminaAbility.canceled += context => IlluminaAbilityInputInactive();
        #endregion

        gamepadTest.Player.PushLeftStick.performed += context => PushLeftStickInputActive();
        gamepadTest.Player.PushLeftStick.canceled += context => PushLeftStickInputInactive();

        gamepadTest.Player.PushRightStick.performed += context => PushRightStickInputActive();
        gamepadTest.Player.PushRightStick.canceled += context => PushRightStickInputInactive();
    }

    // Update is called once per frame
    private void Update()
    {
        if (move == Vector2.zero)
        {
            LeftStickInputInactive();
        }
        else
        {
            LeftStickInputActive();
        }

        if (look == Vector2.zero)
        {
            RightStickInputInactive();
        }
        else
        {
            RightStickInputActive();
        }
    }

    #region Button Functions

    #region South
    private void JumpInputActive()
    {
        buttonSouthPressed.SetActive(true);
    }

    private void JumpInputInactive()
    {
        buttonSouthPressed.SetActive(false);
    }
    #endregion
    #region North
    private void SecondaryAttackInputActive()
    {
        buttonNorthPressed.SetActive(true);
    }

    private void SecondaryAttackInputInactive()
    {
        buttonNorthPressed.SetActive(false);
    }
    #endregion
    #region West
    private void MeleeAttackInputActive()
    {
        buttonWestPressed.SetActive(true);
    }

    private void MeleeAttackInputInactive()
    {
        buttonWestPressed.SetActive(false);
    }
    #endregion
    #region East
    private void UseItemInputActive()
    {
        buttonEastPressed.SetActive(true);
    }

    private void UseItemInputInactive()
    {
        buttonEastPressed.SetActive(false);
    }
    #endregion

    #endregion
    #region DPad Functions

    #region Down
    private void SelectItemDownInputActive()
    {
        dpadDownPressed.SetActive(true);
    }

    private void SelectItemDownInputInactive()
    {
        dpadDownPressed.SetActive(false);
    }
    #endregion
    #region Up
    private void SelectItemUpInputActive()
    {
        dpadUpPressed.SetActive(true);
    }

    private void SelectItemUpInputInactive()
    {
        dpadUpPressed.SetActive(false);
    }
    #endregion
    #region Left
    private void SelectIlluminaAbilityInputActive()
    {
        dpadLeftPressed.SetActive(true);
    }

    private void SelectIlluminaAbilityInputInactive()
    {
        dpadLeftPressed.SetActive(false);
    }
    #endregion
    #region Right
    private void SelectMalayaAbilityInputActive()
    {
        dpadRightPressed.SetActive(true);
    }

    private void SelectMalayaAbilityInputInactive()
    {
        dpadRightPressed.SetActive(false);
    }
    #endregion

    #endregion
    #region Misc Functions
    private void PauseInputActive()
    {
        pausePressed.SetActive(true);
    }

    private void PauseInputInactive()
    {
        pausePressed.SetActive(false);
    }

    private void SelectInputActive()
    {
        selectPressed.SetActive(true);
    }

    private void SelectInputInactive()
    {
        selectPressed.SetActive(false);
    }

    private void TouchpadInputActive()
    {
        touchpadPressed.SetActive(true);
    }

    private void TouchpadInputInactive()
    {
        touchpadPressed.SetActive(false);
    }
    #endregion
    #region Analog Stick Functions
    private void LeftStickInputActive()
    {
        //print(move);

        //leftStick.transform.position = new Vector3(move.x, move.y, 0.0f);
        leftStick.transform.position = leftStickInitPos.transform.position + new Vector3(move.x * 10, move.y * 10, 0.0f);
       
    }

    private void LeftStickInputInactive()
    {
        //print(move);

        leftStick.transform.position = leftStickInitPos.transform.position;
    }

    private void RightStickInputActive()
    {
        //print(look);

        //leftStick.transform.position = new Vector3(move.x, move.y, 0.0f);
        rightStick.transform.position = rightStickInitPos.transform.position + new Vector3(look.x * 10, look.y * 10, 0.0f);

    }

    private void RightStickInputInactive()
    {
        //print(look);

        rightStick.transform.position = rightStickInitPos.transform.position;
    }
    #endregion
    #region Bumpers
    private void DashInputActive()
    {
        rbPressed.SetActive(true);
    }

    private void DashInputInactive()
    {
        rbPressed.SetActive(false);
    }

    private void MapInputActive()
    {
        lbPressed.SetActive(true);
    }

    private void MapInputInactive()
    {
        lbPressed.SetActive(false);
    }

    #endregion
    #region Trigger Functions
    private void MalayaAbilityInputActive()
    {
        rtPressed.SetActive(true);
    }

    private void MalayaAbilityInputInactive()
    {
        rtPressed.SetActive(false);
    }

    private void IlluminaAbilityInputActive()
    {
        ltPressed.SetActive(true);
    }

    private void IlluminaAbilityInputInactive()
    {
        ltPressed.SetActive(false);
    }
    #endregion
    #region Stick Press
    private void PushLeftStickInputActive()
    {
        leftStick.GetComponent<Image>().color = Color.black;
    }

    private void PushLeftStickInputInactive()
    {
        leftStick.GetComponent<Image>().color = Color.white;
    }

    private void PushRightStickInputActive()
    {
        rightStick.GetComponent<Image>().color = Color.black;
    }

    private void PushRightStickInputInactive()
    {
        rightStick.GetComponent<Image>().color = Color.white;
    }
    #endregion

    #region Enable & Disable Functions

    private void OnEnable()
    {
        gamepadTest.Player.Enable();
    }

    private void OnDisable()
    {
        gamepadTest.Player.Disable();
    }

    #endregion
}
