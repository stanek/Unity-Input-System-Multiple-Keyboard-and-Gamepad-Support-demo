using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;

    private static int _playerNumber = 0;

    private PlayerInputControls _controls;

    private List<InputControl> _alreadyJoinedGamepads = new List<InputControl>();

    public void Awake()
    {
        _controls = new PlayerInputControls();
    }
    
    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
    
    private void Start()
    {
        //If you would like to set a max player count - you need to add a PlayerInputManager Component
        
        //As of 1.0.0 - PlayerInputManager.instance.maxPlayerCount setter has not been implemented, marked as TODO
        //Guess you could use our _playerNumber variable in the meantime.
        
        //PlayerInputManager.instance.maxPlayerCount = 2;

        _controls.Player.JoinKeyboard_1.performed += JoinKeyboard_1;  //Polls WASDF keys - Stops Polling once player has joined
        _controls.Player.JoinKeyboard_2.performed += JoinKeyboard_2;  //Polls IJKLH keys - Stops Polling once player has joined
        _controls.Player.JoinGamepad.performed += JoinGamepad;        //Polls for Start Button on Gamepads - Always polls.
                                                                      //Checks to see if device has already joined
    }

    private void JoinKeyboard_1(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Connecting: {ctx.ToString()}");

        _playerNumber++;
        
        var go = PlayerInput.Instantiate(
            playerPrefab,                 //GameObject to instantiate - Must contain a PlayerInput Component
            _playerNumber,                //playerIndex - Must be unique
            "Keyboard1",    //String must match Control Scheme name set in your InputActions
            -1,            //splitScreenIndex. -1 no split screen
            ctx.control.device            //If you have multiple Keyboard players you must implicitly specific the device to use
                                          //otherwise only the first Joined keyboard will have the keyboard device associated with it.
            );
        go.transform.parent = gameObject.transform;
        
        //Unregister so we don't add this Control Scheme again
        _controls.Player.JoinKeyboard_1.performed -= JoinKeyboard_1;
    }
    
    private void JoinKeyboard_2(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Connecting: {ctx.ToString()}");
        
        _playerNumber++;
        
        var go = PlayerInput.Instantiate(
            playerPrefab,               //GameObject to instantiate - Must contain a PlayerInput Component
            _playerNumber,              //playerIndex - Must be unique
            "Keyboard2",  //String must match Control Scheme name set in your InputActions
            -1,          //splitScreenIndex. -1 no split screen
            ctx.control.device          //If you have multiple Keyboard players you must implicitly specific the device to use
                                        //otherwise only the first Joined keyboard will have the keyboard device associated with it.
            );
        go.transform.parent = gameObject.transform;
        
        //Unregister so we don't add this Control Scheme again
        _controls.Player.JoinKeyboard_2.performed -= JoinKeyboard_2;
    }
    
    private void JoinGamepad(InputAction.CallbackContext ctx)
    {
        foreach (InputControl gamepad in _alreadyJoinedGamepads)
        {
            if (gamepad.device.deviceId == ctx.control.device.deviceId)
            {
                return;    //This device is already connected
            }
        }
        
        _alreadyJoinedGamepads.Add(ctx.control.device);

        Debug.Log($"Connecting: {ctx.ToString()}");
        
        _playerNumber++;

        var go = PlayerInput.Instantiate(
            playerPrefab,                 //GameObject to instantiate - Must contain a PlayerInput Component
            _playerNumber,                //playerIndex - Must be unique
            "Gamepad",      //String must match Control Scheme name set in your InputActions
            -1,            //splitScreenIndex. -1 no split screen
            ctx.control.device            //I've only been able to test with 2 different types of Gamepads.
                                          //Don't know if you can connect 2 of the same types of controllers like this.
                                          //Left in just in case, doesn't hurt.
        );
        go.transform.parent = gameObject.transform;
    }
}
