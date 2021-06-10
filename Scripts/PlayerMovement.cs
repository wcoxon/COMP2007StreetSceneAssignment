using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class PlayerMovement : MonoBehaviour
{

    public float walkAcceleration;
    public float sprintAcceleration;

    public float walkDrag;
    public Vector3 inputVector;
    public bool onGround;
    public float jumpSpeed;
    public float gravity;

    public Transform headbob;
    public Transform lean;
    public Transform screenShake;

    public float leanFactor;

    public float mouseSpeed;

    public List<Collision> collisions;
    public Vector3 velocity;

    public static float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;

        collisions = new List<Collision>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame

    public bool isGrounded(float dist)
    {    // Check if in contact with ground
        //float distToGround = 1f;
        bool groundCheck = Physics.Raycast(transform.position, -Vector3.up, dist);
        return groundCheck;
    }

    private void FixedUpdate()
    {
        //GetComponent<CharacterController>().Move(velocity*Time.fixedDeltaTime);
    }

    void Update()
    {

        
        onGround = GetComponent<CharacterController>().isGrounded;
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y+ mouseSpeed*Input.GetAxis("Mouse X"),0);
        Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x + -mouseSpeed * Input.GetAxis("Mouse Y"),0, 0);

        if (onGround)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                velocity += transform.TransformDirection(inputVector.normalized * sprintAcceleration * Time.deltaTime);
                velocity -= velocity * walkDrag * Time.deltaTime;
            }
            else
            {
                velocity += transform.TransformDirection(inputVector.normalized * walkAcceleration * Time.deltaTime);
                velocity -= velocity * walkDrag * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity += Vector3.up * jumpSpeed;
            }
        }
        else
        {
            velocity -= Vector3.up * gravity * Time.deltaTime;
        }
        GetComponent<CharacterController>().Move(velocity * Time.deltaTime);

        Camera.main.transform.localPosition = new Vector3(0, 1.2f, 0);
        Camera.main.transform.localPosition += headbob.localPosition;
        Camera.main.transform.localPosition += screenShake.localPosition;
    }
}
