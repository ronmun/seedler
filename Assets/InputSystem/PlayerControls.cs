// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""CameraInput"",
            ""id"": ""1c4ff769-023e-4620-b91e-bd68f5a4d4ec"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""8cf77d42-86b4-44f2-aecb-5ff0b5bd4536"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mode"",
                    ""type"": ""Button"",
                    ""id"": ""64f045b7-a2cd-483a-a458-df31d2843fad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c7cb715f-c96d-4874-9a48-a0606f522c39"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f694200b-af88-4ef9-a006-09ea904d9a69"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerInput"",
            ""id"": ""be9e4f5c-bd04-491e-b232-cf0ae30923b7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""4aa3ce86-0a0c-4945-a247-933760357f39"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""2ea1ff6a-2f60-40f9-8449-6e86e0715709"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""42dc67d2-d131-49c8-854e-79cc683a2dfa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2c9c336a-f459-43a1-9521-c6b9f24c7347"",
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
                    ""id"": ""604b57af-4860-4197-9a1a-244df9ae9816"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c174d17-767d-4354-a633-8f7949cb30f7"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CameraInput
        m_CameraInput = asset.FindActionMap("CameraInput", throwIfNotFound: true);
        m_CameraInput_Rotate = m_CameraInput.FindAction("Rotate", throwIfNotFound: true);
        m_CameraInput_Mode = m_CameraInput.FindAction("Mode", throwIfNotFound: true);
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_Move = m_PlayerInput.FindAction("Move", throwIfNotFound: true);
        m_PlayerInput_Run = m_PlayerInput.FindAction("Run", throwIfNotFound: true);
        m_PlayerInput_Pause = m_PlayerInput.FindAction("Pause", throwIfNotFound: true);
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

    // CameraInput
    private readonly InputActionMap m_CameraInput;
    private ICameraInputActions m_CameraInputActionsCallbackInterface;
    private readonly InputAction m_CameraInput_Rotate;
    private readonly InputAction m_CameraInput_Mode;
    public struct CameraInputActions
    {
        private @PlayerControls m_Wrapper;
        public CameraInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_CameraInput_Rotate;
        public InputAction @Mode => m_Wrapper.m_CameraInput_Mode;
        public InputActionMap Get() { return m_Wrapper.m_CameraInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraInputActions set) { return set.Get(); }
        public void SetCallbacks(ICameraInputActions instance)
        {
            if (m_Wrapper.m_CameraInputActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_CameraInputActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_CameraInputActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_CameraInputActionsCallbackInterface.OnRotate;
                @Mode.started -= m_Wrapper.m_CameraInputActionsCallbackInterface.OnMode;
                @Mode.performed -= m_Wrapper.m_CameraInputActionsCallbackInterface.OnMode;
                @Mode.canceled -= m_Wrapper.m_CameraInputActionsCallbackInterface.OnMode;
            }
            m_Wrapper.m_CameraInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Mode.started += instance.OnMode;
                @Mode.performed += instance.OnMode;
                @Mode.canceled += instance.OnMode;
            }
        }
    }
    public CameraInputActions @CameraInput => new CameraInputActions(this);

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_Move;
    private readonly InputAction m_PlayerInput_Run;
    private readonly InputAction m_PlayerInput_Pause;
    public struct PlayerInputActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerInput_Move;
        public InputAction @Run => m_Wrapper.m_PlayerInput_Run;
        public InputAction @Pause => m_Wrapper.m_PlayerInput_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRun;
                @Pause.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    public interface ICameraInputActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnMode(InputAction.CallbackContext context);
    }
    public interface IPlayerInputActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
