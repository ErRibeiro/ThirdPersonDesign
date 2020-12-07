using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class RigidbodyCharacterController : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce = 10;
    [SerializeField]
    private float maxSpeed = 2;

    [SerializeField]
    [Tooltip("How fast the player turns. 0 = no turning, 1 = instant snap turning")]
    [Range(0, 1)]
    private float turnSpeed = 0.1f;
    private new Rigidbody rigidbody;
    private Vector2 input;
    private new Collider collider;

    [SerializeField]
    private PhysicMaterial stoppingPhysicsMaterial, movingPhysicsMaterial;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    private void FixedUpdate()
    {

        Vector3 cameraRelativeInputDirection = GetCameraRelativeInputDirection();

        UpdatePhysicsMaterial();
        Move(cameraRelativeInputDirection);
        RotateToFaceMoveInputDirection(cameraRelativeInputDirection);

    }

    private void RotateToFaceMoveInputDirection(Vector3 cameraRelativeInputDirection)
    {
        if (cameraRelativeInputDirection.magnitude > 0)
        {
            var targetRotation = Quaternion.LookRotation(cameraRelativeInputDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed);
        }
    }

    /// <summary>
    /// Moves the player in a direction based on its max speed and acceleration.
    /// </summary>
    /// <param name="moveDirection"></param>
    private void Move(Vector3 moveDirection)
    {
        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(moveDirection * accelerationForce, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Updates physics material to low friction is moving and high if trying to stop.
    /// </summary>
    private void UpdatePhysicsMaterial()
    {
        collider.material = input.magnitude > 0 ? movingPhysicsMaterial : stoppingPhysicsMaterial;
    }

    /// <summary>
    /// Uses the input vector to create a camera relative verison
    /// so the player can move based on camera's forward.
    /// </summary>
    /// <returns>Returns camera's relative input direction</returns>
    private Vector3 GetCameraRelativeInputDirection()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);

        Vector3 cameraFlattenedForward = Camera.main.transform.forward;
        cameraFlattenedForward.y = 0;
        var cameraRotation = Quaternion.LookRotation(cameraFlattenedForward);
        Vector3 cameraRelativeInputDirectionToReturn = cameraRotation * inputDirection;
        return cameraRelativeInputDirectionToReturn;
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
    /// <summary>
    /// This event handler is called from the Player Input component using the new Input System.
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
