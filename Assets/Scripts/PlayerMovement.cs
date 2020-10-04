using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float smoothRotationSpeed = 0.1f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private Transform cam;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private LayerMask obstacleMasks;

    private CharacterController characterController;
    private float smoothVelocity;
    private bool isGrounded;

    private Animator animator;
    private string jumpTrigger = "jump";
    private string crouchAnimatorBool = "isCrouching";

    private Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        AdjustGravity();

        if(Input.GetButtonDown("Jump") && isGrounded)
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

    private void Jump()//Invoked in Animator
    {
        velocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
    }

    private IEnumerator AddForce(Vector3 direction, float forcePower, float forceTime)
    {
        velocity = direction * forcePower;
        yield return new WaitForSeconds(forceTime);
        velocity = Vector3.zero;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (LayerMaskCheck.IsInLayerMask(hit.gameObject, obstacleMasks))
        {
            Vector3 direction = (transform.position - hit.transform.position).normalized;
            Obstacle swingingObstacle = hit.transform.GetComponent<Obstacle>();
            StartCoroutine(AddForce(direction, swingingObstacle.pushPower, swingingObstacle.pushTime));
        }
    }
}
