using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody theRB;
    public float forwardAccel = 8f, 
        reverseAccel = 4f, 
        maxSpeed = 50f, 
        turnStrength = 180,
        gravityForce = 10f,
        dragOnGround = 3f;

    private float speedInput, turnInput;

    private bool grounded;

    public float VelKM;

    public LayerMask whatIsGround;
    public float groundRayLenght = .5f;
    public Transform groundRayPoint;

    void Start()
    {
        theRB.transform.parent = null;
    }

    void Update()
    {
        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");

        if(grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        transform.position = theRB.transform.position;
    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        VelKM = theRB.velocity.magnitude * 3.6f;

        if (Physics.Raycast(groundRayPoint.position, - transform.up, out hit, groundRayLenght, whatIsGround)) 
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (grounded)
        {
            theRB.drag = dragOnGround;

            if (Mathf.Abs(speedInput) > 0)
            {
                theRB.AddForce(transform.forward * speedInput);
            }
        } 
        else
        {
            theRB.drag = 0.1f;

            theRB.AddForce(Vector3.up * -gravityForce * 100f);
        }

    }
}