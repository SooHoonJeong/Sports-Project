using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float gravity = 5f;
    [SerializeField] private float jumpHeight = 0.775f;

    private CharacterController controller;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float horizontal = 0f;
        float vertical = 0f;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) horizontal -= 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) horizontal += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) vertical -= 1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) vertical += 1f;

        bool sprinting = keyboard.leftShiftKey.isPressed || keyboard.rightShiftKey.isPressed;
        float currentSpeed = sprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        Vector3 horizontalMove = direction.normalized * currentSpeed;

        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f) verticalVelocity = -2f;

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);
            }
        }

        verticalVelocity -= gravity * Time.deltaTime;

        Vector3 move = (horizontalMove + Vector3.up * verticalVelocity) * Time.deltaTime;
        controller.Move(move);
    }
}
