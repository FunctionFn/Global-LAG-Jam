using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;

    public string PlayerNumber;

    public Vector3 moveDirection;

    public float speed;
    public float jumpSpeed;
    public float airSpeedModifier;

    public float lookSpeedH;
    public float lookSpeedV;

    public float jumpHoldGravityModifier;
    public float gravity;



    bool hasJustLanded = false;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();

        moveDirection = new Vector3(0, 0, 0);

        Physics.IgnoreLayerCollision(8, gameObject.layer);

    }

    // Update is called once per frame
    void Update()
    {

        if (controller.isGrounded)
        {
            if (!hasJustLanded)
            {
                hasJustLanded = true;
            }
        }
        else
        {
            hasJustLanded = false;
        }

        ControlUpdate();

        if (Input.GetButton("Fire" + PlayerNumber))
        {

        }
        

    }

    void ControlUpdate()
    {
        float speedMod = 1;


        if (!controller.isGrounded)
        {
            speedMod = airSpeedModifier;
        }
        moveDirection = new Vector3(Input.GetAxis("Horizontal" + PlayerNumber) * speedMod, moveDirection.y, Input.GetAxis("Vertical" + PlayerNumber));

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        }


        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = new Vector3(moveDirection.x * speed, moveDirection.y, moveDirection.z * speed);



        if (controller.isGrounded)
        {

            if (Input.GetButtonDown("Jump" + PlayerNumber))
            {
                moveDirection.y = jumpSpeed;
            }

        }

        if (moveDirection.y > 0 && Input.GetButton("Jump" + PlayerNumber))
            moveDirection.y -= (gravity - jumpHoldGravityModifier) * Time.deltaTime;
        else
            moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //CameraControl();
    }

    //void CameraControl()
    //{
    //    lookDirectionH = new Vector3(0, Input.GetAxis("Horizontal2"), 0);
    //    transform.Rotate(lookDirectionH * lookSpeedH);


    //    lookDirectionV = new Vector3(Input.GetAxis("Vertical2"), 0, 0);
    //    cameraAxisLocation.Rotate(lookDirectionV * lookSpeedV);
    //    //Debug.Log(cameraAxisLocation.rotation.eulerAngles.x);

    //    if (cameraAxisLocation.rotation.eulerAngles.x > 270)
    //    {
    //        cameraAxisLocation.localEulerAngles = new Vector3(Mathf.Clamp(cameraAxisLocation.rotation.eulerAngles.x, 280, 400), cameraAxisLocation.rotation.y, cameraAxisLocation.rotation.z);
    //    }
    //    else
    //    {
    //        cameraAxisLocation.localEulerAngles = new Vector3(Mathf.Clamp(cameraAxisLocation.rotation.eulerAngles.x, -10, 80), cameraAxisLocation.rotation.y, cameraAxisLocation.rotation.z);
    //    }




    //    /*
    //    lookDirectionV = Input.GetAxis("Vertical2") * lookSpeedV;
    //    cameraRotationAxis = rightSide.position - transform.position;
    //    mainCamera.transform.RotateAround(transform.position, cameraRotationAxis, lookDirectionV);
    //     */

    //}

    //*
    void OnTriggerEnter(Collider other)
    {

       
    }

    void OnTriggerExit(Collider collider)
    {
        
    }
    //*/



    

    void OnTriggerStay(Collider other)
    {
        
    }

    public bool StolenIsPlayerMoving()
    {
        if (Mathf.Abs(controller.velocity.magnitude) >= .5)
            return true;
        else
            return false;
    }
}
