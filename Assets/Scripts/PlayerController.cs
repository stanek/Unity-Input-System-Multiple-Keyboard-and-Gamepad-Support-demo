using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputControls _controls;

    private Vector2 _movementInput;
    private void Awake()
    {
        gameObject.name = $"Player {GetComponent<PlayerInput>().playerIndex.ToString()}";
        _controls = new PlayerInputControls();
        
        //Assign a performed action to a function
        _controls.Player.Attack.performed += ctx => AnotherWayToAttack();
        
        //Assign the results of an Action to a variable
        //This action is set as pass through so it is constantly read and updated.
        _controls.Player.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero)
        {
            Debug.Log($"{gameObject.name} {_movementInput.ToString()}");
        }
    }
    
    //The InputAction named Attack can be accessed through OnAttack()
    //This is somehow linked because this GameObject also contains a PlayerInput Component
    //In this case it is done through MessagePassing
    private void OnAttack()
    {
        Debug.Log($"{gameObject.name} - OnAttack() - Attack");
    }
    
    private void AnotherWayToAttack()
    {
        Debug.Log($"{gameObject.name} - AnotherWayToAttack() - Another way to Attack");
    }
    
    //The InputAction named Move can be accessed through OnMove()
    //This is somehow linked because this GameObject also contains a PlayerInput Component
    
    //This is probably not the way to continuously move around.
    //There is no way to 'read' the Vector of this action this way.  The parameter list is always empty.
    //Even when the Action is set as Pass-through; this way of calling a function through 'MessagePassing"
    //only tells you when one of the buttons were clicked.
    private void OnMove()
    {
        Debug.Log($"{gameObject.name} - Move Started");
    }
}
