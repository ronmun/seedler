using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rb;
    public float playerSpeed;
    public float runingSpeed;
    public float rotationSpeed;

    public CameraScript cameraScript;
    private Vector3 velocity;
    private float vSpeed = 0;

    [HideInInspector]
    public bool canClimb = false;

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
        //controller = gameObject.GetComponent<CharacterController>();
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!canClimb)
            CameraPositionMovement(movement.y, movement.x);
        else
            Climb();
    }

    /*void CameraPositionMovement(float vertical, float horizontal)
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
    }*/

    /*void CameraPositionMovement(float vertical, float horizontal)   //Movimiento sin RigidBody, con Move y gravedad ajustable.
    {
        Vector3 translation = vertical * cameraScript.transform.forward;
        translation += horizontal * cameraScript.transform.right;

        if (translation.magnitude > 0)
            velocity = translation;
        else
            velocity = Vector3.zero;

        velocity = velocity * playerSpeed;

        if(!controller.isGrounded)
        {
            vSpeed -= 29.81f * Time.deltaTime;
            velocity.y = vSpeed;
        }
        else
        {
            vSpeed = 0;
        }

        controller.Move(velocity * Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position * velocity.magnitude), Time.deltaTime * rotationSpeed);
        if (vertical != 0 || horizontal != 0)
        {
            Vector3 direction = new Vector3(velocity.x, 0f, velocity.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * rotationSpeed);
        }
    }*/

    void CameraPositionMovement(float vertical, float horizontal)   //Movimiento con RigidBody
    {
        Vector3 translation = vertical * new Vector3(cameraScript.transform.forward.x, 0f, cameraScript.transform.forward.z);
        translation += horizontal * cameraScript.transform.right;

        if (translation.magnitude > 0)
            velocity = translation;
        else
            velocity = Vector3.zero;

        int layerMask = LayerMask.NameToLayer("Default");
        layerMask = 1 << layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, velocity.normalized, out hit, 1.2f, layerMask))
        {
            Debug.DrawRay(transform.position, velocity.normalized * hit.distance, Color.red);
            Debug.Log("Hit");
        }
        else
        {
            velocity = velocity * playerSpeed;
            rb.MovePosition(transform.position + (velocity * Time.deltaTime));
            Debug.Log("Did not hit");
            Debug.DrawRay(transform.position, velocity.normalized * 1.2f, Color.white);
        }

        if (vertical != 0 || horizontal != 0)
        {
            Vector3 direction = new Vector3(velocity.x, 0f, velocity.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * rotationSpeed);
        }
    }

    void Climb()
    {
        //rb.useGravity = false;

        if (movement.y > 0.1)
            rb.MovePosition(transform.position + Vector3.up * Time.deltaTime * playerSpeed);
            //transform.Translate( Vector3.up * Time.deltaTime * playerSpeed);
        if (movement.y < -0.1)
            rb.MovePosition(transform.position + Vector3.down * Time.deltaTime * playerSpeed);
            //transform.Translate(Vector3.down * Time.deltaTime * playerSpeed);
    }

    private void OnEnable()
    {
        controls.PlayerInput.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerInput.Disable();
    }
}
