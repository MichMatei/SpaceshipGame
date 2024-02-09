using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement playerMovementInstance;
    public CharacterController controller;
    public GameMenus gameMenus;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    //public TasksScript taskMenuscript;
    public bool taskMenusActive = false;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public void Awake()
    {
        if (playerMovementInstance == null)
        {
            playerMovementInstance = this;
            DontDestroyOnLoad(playerMovementInstance);
        }
        else
        {
            Debug.Log("destro");//this needs a fix and into playermovement.cs
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if(Input.GetKeyDown(KeyCode.P))
        {
            gameMenus.PauseOnOff();
        }
        
        if(!taskMenusActive)
        {
            //taskMenuscript.Awake();
            //taskMenusActive = true;
        }
    }
}
