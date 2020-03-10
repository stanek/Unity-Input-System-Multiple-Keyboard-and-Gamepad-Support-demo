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
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    //The InputAction named Attack can be accessed through OnAttack()
    //This is somehow linked because this GameObject also contains a PlayerInput Component
    //In this case it is done through MessagePassing
    private void OnAttack()
    {
        Debug.Log($"{gameObject.name} - OnAttack() - Attack");
    }

    private void OnMove(InputValue ctx)
    {
        Debug.Log($"{gameObject.name} - {ctx.Get<Vector2>().ToString()}");
    }
}
