using System.Collections;
using UnityEngine;

#pragma warning disable CS0649

/// <summary>
/// This script is responsible Player movement
/// Player can move, jump and crouch.
///
/// How to use it:
/// Drag Player prefab and Third Person Camera prefab to the scene.
/// You have to put camera object to the cam variable.
/// Third Person Camera prefab is already set. 
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Third person camera"), SerializeField] private Transform cam;
    [Tooltip("Speed with which we drag player to the ground"), SerializeField] private float gravity = -9.81f;

    [Header("Movement")]
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float smoothRotationSpeed = 0.1f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask swingingObstacleMask;
    [SerializeField] private LayerMask spikeMask;
    [SerializeField] private LayerMask rotatingObstacleMasks;
    [SerializeField] private LayerMask gameManagerMask;

    private CharacterController characterController;
    private GameManager gameManager;
    private Animator animator;

    private float smoothVelocity;
    /// <summary>
    /// Defines if player is on ground
    /// </summary>
    private bool isGrounded;
    /// <summary>
    /// Defines if player can move
    /// </summary>
    public static bool isBlocked;

    private Vector3 velocity;

    /// <summary>
    /// name of jump trigger parameter from animator
    /// </summary>
    private string jumpTrigger = "jump";
    /// <summary>
    /// name of crouch bool parameter from animator
    /// </summary>
    private string crouchAnimatorBool = "isCrouching";

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();

        isBlocked = true;
    }

    private void Update()
    {
        if (!isBlocked)
        {
            Move();
        }
        AdjustGravity();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger(jumpTrigger);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool(crouchAnimatorBool, true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool(crouchAnimatorBool, false);
        }
    }

    /// <summary>
    /// Moves and rotates Player in XZ Axis
    /// </summary>
    private void Move()
    {
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        if(direction.magnitude > 0)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVelocity, smoothRotationSpeed);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 moveDirection = (Quaternion.Euler(0, angle, 0) * Vector3.forward).normalized;
            characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Set Gravity to our player. It drags player to the ground when he is not grounded.
    /// </summary>
    private void AdjustGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -0.1f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Makes player Jump
    /// It's invoked in jump animation
    /// </summary>
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
    }

    /// <summary>
    /// Adds Force to player for chosen time
    /// </summary>
    /// <param name="direction">direction of force</param>
    /// <param name="forcePower">Power of force</param>
    /// <param name="forceTime">time in which player will be forced to move</param>
    /// <returns></returns>
    private IEnumerator AddForce(Vector3 direction, float forcePower, float forceTime)
    {
        velocity = direction * forcePower;
        yield return new WaitForSeconds(forceTime);
        velocity = Vector3.zero;
    }

    /// <summary>
    /// Handles all collisions
    /// </summary>
    /// <param name="hit"></param>
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Collision with swinging obstacle
        if (LayerMaskCheck.IsInLayerMask(hit.gameObject, swingingObstacleMask))
        {
            AddForceToPlayer(hit, hit.transform.GetComponentInParent<Obstacle>());
        }
        //Collision with other obstacles, like spike or rotating wall
        if (LayerMaskCheck.IsInLayerMask(hit.gameObject, spikeMask, rotatingObstacleMasks)) 
        {
            AddForceToPlayer(hit, hit.transform.GetComponent<Obstacle>());
        }
    }

    /// <summary>
    /// Adds force to the player in calculated direction.
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="obstacle"></param>
    private void AddForceToPlayer(ControllerColliderHit hit, Obstacle obstacle)
    {
        Vector3 direction = (transform.position - hit.transform.position).normalized;
        Obstacle _obstacle = obstacle;
        StartCoroutine(AddForce(direction, _obstacle.pushPower, _obstacle.pushTime));

        if (_obstacle.isDeadly)
        {
            gameManager.SetFailPopUp();
        }
    }
}
