using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Footsteps : MonoBehaviour
{

    
    public AnimationCurve bobCurve;
    public float defaultCameraHeight = 1.2f;

    public Transform bobTransform;

    public AudioSource stepSound;

    public float stride;
    float stepInterpolation;

    float distanceTravelled;
    float distanceLeft;
    float stepDistance;

    public PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        bool startedWalking = (Input.GetButtonDown("Vertical") && !Input.GetButton("Horizontal")) || (!Input.GetButton("Vertical") && Input.GetButtonDown("Horizontal"));

        float vNormalized = movement.velocity.magnitude * movement.walkDrag / movement.walkAcceleration;//Mathf.Round(movement.velocity.magnitude) * movement.walkDrag / movement.walkAcceleration;
        distanceTravelled += movement.velocity.magnitude * Time.deltaTime;
        distanceLeft = vNormalized * (stride - distanceTravelled);
        stepDistance = distanceTravelled + distanceLeft;
        
        if (stepDistance > 0f)
        {
            stepInterpolation = distanceTravelled / stepDistance;
        }
        
        if ((startedWalking || stepInterpolation > 1f) && GetComponent<CharacterController>().isGrounded)
        {
            distanceTravelled = 0f;
            stepInterpolation = 0f;

            stepSound.pitch = Random.Range(0.8f, 1f);
            stepSound.PlayOneShot(stepSound.clip);
        }
        

        if (GetComponent<CharacterController>().isGrounded)
        {
            //Camera.main.transform.localPosition = new Vector3(0, defaultCameraHeight + (stepDistance/stride)* bobCurve.Evaluate(stepInterpolation), 0);
            bobTransform.localPosition = new Vector3(0, (stepDistance / stride) * bobCurve.Evaluate(stepInterpolation), 0);
        }


    }
}
