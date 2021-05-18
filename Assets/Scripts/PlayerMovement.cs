using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private Rigidbody rb;
    public float playerBaseSpeed;
    public float runingSpeed;
    public float bounceForce = 8f;
    public float rotationSpeed;
    public int gems = 0;

    public CameraScript cameraScript;
    [SerializeField]
    private float playerSpeed;
    private bool isRunning = false;
    private Vector3 velocity;

    [HideInInspector]
    public bool canClimb = false;
    [HideInInspector]
    public List<Transform> stairPoints;
    [HideInInspector]
    public int stairIndex;
    [HideInInspector]
    public Vector3 stairRotation;

    public bool stopMove;

    public GameObject playerModel;

    public float gravityScale = 5f;

    //Controls
    public PlayerControls controls;
    Vector2 movement;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.PlayerInput.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.PlayerInput.Move.canceled += ctx => movement = Vector2.zero;

        controls.PlayerInput.Run.started += ctx => playerSpeed += runingSpeed;
        controls.PlayerInput.Run.started += ctx => isRunning = true;

        controls.PlayerInput.Run.canceled += ctx => playerSpeed -= runingSpeed;
        controls.PlayerInput.Run.canceled += ctx => isRunning = false;

        instance = this;
    }

    private void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        rb = this.GetComponent<Rigidbody>();
        playerSpeed = playerBaseSpeed;
    }

    void FixedUpdate()
    {
        if (!canClimb)
            CameraPositionMovement(movement.y, movement.x);
        else
            Climb();


        // anim.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.z));
        // anim.SetBool("Grounded", controller.isGrounded);
    }

    public void Bounce(){
        // movement.y = bounceForce;
        // controller.Move(movement * Time.deltaTime);
    }

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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(stairRotation, Vector3.up), Time.deltaTime * rotationSpeed * 2);
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

    public IEnumerator SpeedBoost(float speed)
    {
        playerSpeed += speed;
        while(playerSpeed > playerBaseSpeed)
        {
            playerSpeed -= 3f * Time.deltaTime;
            yield return null;
        }

        if (isRunning)
            playerSpeed = playerBaseSpeed + playerSpeed;
        else
            playerSpeed = playerBaseSpeed;
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
        // isKnocking = true;
        // knockbackCounter = knockBackLength;
        // //Debug.Log("Knocked Back");
        // movement.y = knockbackPower.y;
        // controller.Move(movement * Time.deltaTime);
    }
}
