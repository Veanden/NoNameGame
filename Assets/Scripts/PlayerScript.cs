using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Camera cam;
    private PlayerInputActions playerControlls;
    private CharacterController controller;

    private Vector2 movementInput;
    private Vector3 moveDirection;
    private float mouseX;
    private float mouseY;

    private bool sprinting = false;
    private float fieldOfView;

    public GameObject sight;
    [SerializeField] private float adsSpeed = 1f;
    [SerializeField] private float sightOffset = 0.1f;
    private bool inAds = false;
    private bool stoppingAds = false;
    private bool isOnTarget = false;
    private Vector3 targetPosition;
    private Vector3 sightOriginalPosition;
    private float moveTime = 0f;

    [SerializeField] private float swayMultiplier = 1f;
    [SerializeField] private float swaySmoothness = 1f;
    private float swayX;
    private float swayY;

    [Header("Physics")]
    [SerializeField] private float gravity = 9.8f;
    private float velocityY = 0f;
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float sprintSpeed = 1f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;

    private void Start()
    {
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
        fieldOfView = cam.fieldOfView;
        sightOriginalPosition = sight.transform.localPosition;
    }

    private void Awake()
    {
        playerControlls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
    }

    private void OnDisable()
    {
        playerControlls.Disable();
    }

    private void Update()
    {
        updateMouse();
        updateMovement();
        updateSprint();
        UpdateAds();
        weaponSway();
    }

    private void updateSprint()
    {
        if (sprinting)
        {
            if(cam.fieldOfView < fieldOfView + 10)
            {
                cam.fieldOfView += 0.5f;
            }
        }
        else
        {
            if (cam.fieldOfView > fieldOfView)
            {
                cam.fieldOfView -= 0.5f;
            }
        }
    }

    private void updateMouse()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        transform.eulerAngles -= new Vector3(0, -mouseX, 0);
        cam.transform.eulerAngles -= new Vector3(mouseY, 0, 0);
    }

    private void updateMovement()
    {
        movementInput = playerControlls.Player.Move.ReadValue<Vector2>();
        moveDirection = transform.TransformDirection(movementInput.x, 0, movementInput.y);
        moveDirection.y = velocityY;
        moveDirection.x *= sprintSpeed;
        moveDirection.z *= sprintSpeed;
        controller.Move(moveDirection * movementSpeed * Time.deltaTime);

        velocityY -= gravity * Time.deltaTime;

        if (controller.isGrounded)
            velocityY -= gravity * 0.1f;

        if(velocityY < 0)
        {
            velocityY += -gravity * (fallMultiplier - 1) * Time.deltaTime;
        }

    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            if(controller.isGrounded)
                velocityY = jumpVelocity;
        }
    }

    public void Sprint(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            sprintSpeed *= 2;
            sprinting = true;
        }

        if (callbackContext.canceled)
        {
            sprintSpeed /= 2;
            sprinting = false;
        }
    }

    public void UpdateAds()
    {
        moveTime += Time.deltaTime * adsSpeed;

        if (inAds && !isOnTarget)
        {
            targetPosition = cam.transform.position + cam.transform.forward * sightOffset;
            sight.transform.position = Vector3.Lerp(sight.transform.position, targetPosition, moveTime);
            if (sight.transform.localPosition == targetPosition)
                isOnTarget = true;
        }

        if (!inAds && sight.transform.localPosition != sightOriginalPosition && stoppingAds)
        {
            sight.transform.localPosition = Vector3.Lerp(sight.transform.localPosition, sightOriginalPosition, moveTime);
            isOnTarget = false;

            if (stoppingAds && sight.transform.localPosition == sightOriginalPosition)
                stoppingAds = false;
        }
    }

    public void weaponSway()
    {
        swayX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        swayY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        sight.transform.localRotation = Quaternion.Slerp(sight.transform.localRotation, targetRotation, Time.deltaTime * swaySmoothness);
    }

    public void AimDownSight(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            moveTime = 0f;
            inAds = true;
        }

        if(callbackContext.canceled)
        {
            moveTime = 0f;
            inAds = false;
            stoppingAds = true;
        }
    }

}
