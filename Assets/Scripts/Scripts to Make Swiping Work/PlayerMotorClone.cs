using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotorClone : MonoBehaviour
{
    public GameObject enemy;

    private EnemyController motor1;

    private const float LANE_DISTANCE = 2.0f;
    private const float TURN_SPEED = 1.5f;
    //Movement
    private CharacterController controller;
    private float jumpForce = 4.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private int desiredLane = 1;


    //Anim
    private Animator anim;
    

    //Speed
    private float speed;
    private float originalSpeed = 11.0f;
    private float speedIncTick;
    private float speedTime = 2.5f;
    private float speedIncAmount = 0.1f;
    
    private bool isRunning = false;


    //sOUND
    public AudioClip Jump;
    public AudioClip Slide;
  
    public AudioClip Dead;
    public float Volume;
    AudioSource audios;
    
    public bool alreadyPlayed = false;

  
    private void Start()
    {
        audios = GetComponent<AudioSource>();
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

       
        

        



      
    }

    private void Update()
    {
       

        if (!isRunning) 
         return;

        if (Time.time - speedIncTick > speedTime)
        {
            speedIncTick = Time.time;
            speed += speedIncAmount;
            GameManager.Instance.UpdateModifier(speed + originalSpeed);
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
                anim.SetTrigger("Jump");
                audios.PlayOneShot(Jump, Volume);
                alreadyPlayed = true;
                audios.volume = PlayerPrefs.GetFloat("SFX");
                verticalVelocity = jumpForce;
            }

        }
       
        
        {
            verticalVelocity -= (gravity * Time.deltaTime);
            //Fast falling
            if (MobileInputsClone.Instance.SwipeDown)
            {
                StartSlide();
                Invoke("StopSlide", 1.0f);
                audios.PlayOneShot(Slide, Volume);
                alreadyPlayed = true;
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
    private void StartSlide()
    {
        anim.SetBool("Slide", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
       
    }
    private void StopSlide()
    {
        anim.SetBool("Slide", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
        
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
        anim.SetTrigger("Startrun");
       
    }
    public void StopRunning()
    {
        isRunning = false;
        anim.SetTrigger("StopRunning");
        

    }
    public void Take()
    {
        enemy.SetActive(true);
    }
    public void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.Instance.OnDeath();
        audios.volume = PlayerPrefs.GetFloat("Stops");
        Invoke("Take",2.0f);
        





        if (audios)
        {
            audios.volume = 1;
            audios.PlayOneShot(Dead, Volume);
            audios.volume = PlayerPrefs.GetFloat("SFX");
        }
        

    }
    public void STOP()
    {
        isRunning = false;
        anim.SetTrigger("StopRunning");
        GameManager.Instance.OnStopPlayer();
       
    }
   public void Plays()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
        GameManager.Instance.StartPlayer();
    }
     
    
  
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
            case "Enemy":
                Crash();
           break;
            case "STOPS":
                STOP();
                break;
        }
    }

   
    
 

}