using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    private const float LANE_DISTANCE = 2.0f;
    private const float TURN_SPEED = 0.5f;
    //Movement
    private CharacterController controller;
    private float jumpForce = 4.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private int desiredLane = 1;
    private EnemyController motor1;
    //Anim
    private Animator anim;

    //Speed
    private float speed;
    private float originalSpeed = 9.0f;
    private float speedIncTick;
    private float speedTime = 2.5f;
    private float speedIncAmount = 0.1f;

    private bool isRunning = false;
    private void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        
       
    }

    private void Update()
    {

        if (!isRunning)

            return;
        
        if (Time.time - speedIncTick > speed - speedTime)
        {
            speedIncTick = Time.time;
            speed += speedIncAmount;


        }
        //Gather the Input on which lane

        if (MobileInputsClone.Instance.SwipeLeft)
            MoveLane(false);
        if (MobileInputsClone.Instance.SwipeRight)
            MoveLane(true);

        // Calculate
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;
        //Movevectorcalculate
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        //Calculate Y
        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        if (IsGrounded())
        {
            verticalVelocity = -0.1f;

            if (MobileInputsClone.Instance.SwipeUp)
            {
               
                verticalVelocity = jumpForce;
            }

        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);
            //Fast falling
            if (MobileInputsClone.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;
            }
        }



        moveVector.y = verticalVelocity;
        moveVector.z = speed;




        //Move the Player
        controller.Move(moveVector * Time.deltaTime);
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }

    }

    private void MoveLane(bool goingright)
    {
        desiredLane += (goingright) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);


    }
    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);
        return Physics.Raycast(groundRay, 0.2f + 0.1f);


    }
    public void StartRunning()
    {
        isRunning = true;
        anim.SetBool("Startrun",isRunning);
        
    }
    public void StopRunning()
    {
        isRunning = false;
        anim.SetTrigger("Startrun");

    }
    private void Capture()
    {
        anim.SetTrigger("Capture");
        anim.SetTrigger("Take");
       isRunning = false;
        GameManager.Instance.isDead = true;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            
            case "Player":
                Capture();

                break;
        }
    }
   



    


    }
