// GENERATED AUTOMATICALLY FROM 'Assets/PlaystationInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlaystationInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlaystationInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlaystationInput"",
    ""maps"": [
        {
            ""name"": ""MainMenuActionMap"",
            ""id"": ""1a13df1d-b153-489d-ac7a-b1457dfbb308"",
            ""actions"": [
                {
                    ""name"": ""Options"",
                    ""type"": ""Button"",
                    ""id"": ""4738a0c6-3298-43a6-a132-7598774334a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""e14525da-5669-409d-bf92-7d78cc6d186b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""93e9ef11-61b2-4aa4-b7c7-982daba56125"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""66552bf4-7cb3-43c2-9255-5e583e765c7f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d0967cb2-415e-4ecd-898b-aeca2ace925c"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Options"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9169d88-3ad3-4bc5-89c0-44300b516186"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d054080b-b0b4-4775-b129-75f69a038bc5"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74a088e7-bf9a-4e74-b99e-b0f230800de2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b3b689c-28fd-4945-8727-13a9bf8c6ea8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CinematicActionMap"",
            ""id"": ""f7aad63a-b141-4fb4-851a-1b74d0b6ca78"",
            ""actions"": [
                {
                    ""name"": ""Skip"",
                    ""type"": ""Value"",
                    ""id"": ""14e70b08-c1e2-413a-b196-2468749d1f51"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""33d48ed9-2de6-4c04-afc3-243efb027b79"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainMenuActionMap
        m_MainMenuActionMap = asset.FindActionMap("MainMenuActionMap", throwIfNotFound: true);
        m_MainMenuActionMap_Options = m_MainMenuActionMap.FindAction("Options", throwIfNotFound: true);
        m_MainMenuActionMap_Cancel = m_MainMenuActionMap.FindAction("Cancel", throwIfNotFound: true);
        m_MainMenuActionMap_Move = m_MainMenuActionMap.FindAction("Move", throwIfNotFound: true);
        m_MainMenuActionMap_Submit = m_MainMenuActionMap.FindAction("Submit", throwIfNotFound: true);
        // CinematicActionMap
        m_CinematicActionMap = asset.FindActionMap("CinematicActionMap", throwIfNotFound: true);
        m_CinematicActionMap_Skip = m_CinematicActionMap.FindAction("Skip", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // MainMenuActionMap
    private readonly InputActionMap m_MainMenuActionMap;
    private IMainMenuActionMapActions m_MainMenuActionMapActionsCallbackInterface;
    private readonly InputAction m_MainMenuActionMap_Options;
    private readonly InputAction m_MainMenuActionMap_Cancel;
    private readonly InputAction m_MainMenuActionMap_Move;
    private readonly InputAction m_MainMenuActionMap_Submit;
    public struct MainMenuActionMapActions
    {
        private @PlaystationInput m_Wrapper;
        public MainMenuActionMapActions(@PlaystationInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Options => m_Wrapper.m_MainMenuActionMap_Options;
        public InputAction @Cancel => m_Wrapper.m_MainMenuActionMap_Cancel;
        public InputAction @Move => m_Wrapper.m_MainMenuActionMap_Move;
        public InputAction @Submit => m_Wrapper.m_MainMenuActionMap_Submit;
        public InputActionMap Get() { return m_Wrapper.m_MainMenuActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuActionMapActions instance)
        {
            if (m_Wrapper.m_MainMenuActionMapActionsCallbackInterface != null)
            {
                @Options.started -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnOptions;
                @Options.performed -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnOptions;
                @Options.canceled -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnOptions;
                @Cancel.started -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnCancel;
                @Move.started -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnMove;
                @Submit.started -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_MainMenuActionMapActionsCallbackInterface.OnSubmit;
            }
            m_Wrapper.m_MainMenuActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Options.started += instance.OnOptions;
                @Options.performed += instance.OnOptions;
                @Options.canceled += instance.OnOptions;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
            }
        }
    }
    public MainMenuActionMapActions @MainMenuActionMap => new MainMenuActionMapActions(this);

    // CinematicActionMap
    private readonly InputActionMap m_CinematicActionMap;
    private ICinematicActionMapActions m_CinematicActionMapActionsCallbackInterface;
    private readonly InputAction m_CinematicActionMap_Skip;
    public struct CinematicActionMapActions
    {
        private @PlaystationInput m_Wrapper;
        public CinematicActionMapActions(@PlaystationInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Skip => m_Wrapper.m_CinematicActionMap_Skip;
        public InputActionMap Get() { return m_Wrapper.m_CinematicActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CinematicActionMapActions set) { return set.Get(); }
        public void SetCallbacks(ICinematicActionMapActions instance)
        {
            if (m_Wrapper.m_CinematicActionMapActionsCallbackInterface != null)
            {
                @Skip.started -= m_Wrapper.m_CinematicActionMapActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_CinematicActionMapActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_CinematicActionMapActionsCallbackInterface.OnSkip;
            }
            m_Wrapper.m_CinematicActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
            }
        }
    }
    public CinematicActionMapActions @CinematicActionMap => new CinematicActionMapActions(this);
    public interface IMainMenuActionMapActions
    {
        void OnOptions(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
    }
    public interface ICinematicActionMapActions
    {
        void OnSkip(InputAction.CallbackContext context);
    }
}
