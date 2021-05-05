using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public static PlayerMovement instance;
    private Rigidbody rb;
    public float playerSpeed;
    public float runingSpeed;
    public float bounceForce = 8f;
    public float rotationSpeed;

    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;
    public CameraScript cameraScript;
    private Vector3 velocity;
    // private float vSpeed = 0;

    [HideInInspector]
    public bool canClimb = false;
    [HideInInspector]
    public List<Transform> stairPoints;
    [HideInInspector]
    public int stairIndex;

    public bool stopMove;

    public GameObject playerModel;

    public float gravityScale = 5f;

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

        instance = this;
    }

    private void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!isKnocking && !stopMove) {
            float yStore = movement.y;
            movement = (transform.forward * Input.GetAxisRaw("Vertical") + (transform.right * Input.GetAxisRaw("Horizontal")));
            movement.Normalize();
            movement = movement * playerSpeed;
            movement.y = yStore;

        }
        if (!canClimb)
            CameraPositionMovement(movement.y, movement.x);
        else
            Climb();

        if(isKnocking)
        {
            knockbackCounter -= Time.deltaTime;

            float yStore = movement.y;
            movement = playerModel.transform.forward * -knockbackPower.x;
            movement.y = yStore;

            if (controller.isGrounded)
            {
                movement.y = 0f;
            }

            movement.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            controller.Move(movement * Time.deltaTime);

            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        if(stopMove)
        {
            movement = Vector3.zero;
            movement.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            controller.Move(movement);
        }

        // anim.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.z));
        // anim.SetBool("Grounded", controller.isGrounded);
    }

    public void Bounce(){
        movement.y = bounceForce;
        controller.Move(movement * Time.deltaTime);
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
        if (stairPoints.Count > 0)
        {
            if (movement.y > 0.1)
            {
                rb.MovePosition(transform.position + (stairPoints[stairIndex].position - transform.position).normalized * Time.deltaTime * playerSpeed);
                if (Vector3.Distance(this.transform.position, stairPoints[stairIndex].position) < 0.1f)
                {
                    if (stairIndex + 1 == stairPoints.Count)
                        canClimb = false;
                    else
                        stairIndex++;
                }
            }
            if (movement.y < -0.1)
            {
                if(stairIndex == 0)
                    canClimb = false;
                else
                {
                    rb.MovePosition(transform.position + (stairPoints[stairIndex - 1].position - transform.position).normalized * Time.deltaTime * playerSpeed);
                    if (Vector3.Distance(this.transform.position, stairPoints[stairIndex - 1].position) < 0.1f)
                            stairIndex--;
                }
            }
        }
        else
            Debug.Log("Error, list empty");

        if (!canClimb)
            rb.useGravity = true;
    }

    private void OnEnable()
    {
        controls.PlayerInput.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerInput.Disable();
    }

     public void Knockback()
    {
        isKnocking = true;
        knockbackCounter = knockBackLength;
        //Debug.Log("Knocked Back");
        movement.y = knockbackPower.y;
        controller.Move(movement * Time.deltaTime);
    }
}
