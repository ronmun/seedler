using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed;
    public float runingSpeed;
    public float rotationSpeed;

    public CameraScript cameraScript;
    private Vector3 velocity;

    //Controls
    PlayerControls controls;
    Vector2 movement;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.PlayerInput.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.PlayerInput.Move.canceled += ctx => movement = Vector2.zero;

        controls.PlayerInput.Run.started += ctx => playerSpeed += runingSpeed;
        controls.PlayerInput.Run.canceled += ctx => playerSpeed -= runingSpeed;
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        CameraPositionMovement(movement.y, movement.x);
    }

    void CameraPositionMovement(float vertical, float horizontal)
    {
        Vector3 translation = vertical * cameraScript.transform.forward;
        translation += horizontal * cameraScript.transform.right;

        if (translation.magnitude > 0)
            velocity = translation;
        else
            velocity = Vector3.zero;

        controller.SimpleMove(velocity*playerSpeed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position * velocity.magnitude), Time.deltaTime * rotationSpeed);
        if(vertical != 0 || horizontal != 0)
        {
            Vector3 direction = new Vector3(velocity.x, 0f, velocity.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * rotationSpeed);
        }
    }

    /*void WorldPositionMovement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.SimpleMove(move * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }*/

    private void OnEnable()
    {
        controls.PlayerInput.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerInput.Disable();
    }
}
