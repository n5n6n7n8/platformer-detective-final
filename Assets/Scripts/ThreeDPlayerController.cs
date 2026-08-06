using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class ThreeDPlayerController : MonoBehaviour
{


    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    // public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public float lookSensitivity = 2.0f;
    public float topLookLimit = -80.0f;
    public float bottomLookLimit = 80.0f;

    private CharacterController _characterController;
    private Transform _cameraTransform;
    private Vector3 _velocity;
    private float _verticalRotation = 0f;
    private bool _isGrounded;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
        _cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    void HandleMovement()
    {

        //_velocity.y = -2f; // Slight negative force to keep grounded securely


        // Get Input (WASD / Arrow Keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Move relative to character's direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        _characterController.Move(move * moveSpeed * Time.deltaTime);


        // Apply Gravity
        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    void HandleRotation()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotate Body horizontally (Y-Axis)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate Camera vertically (X-Axis) with constraints
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, topLookLimit, bottomLookLimit);
        _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

}
