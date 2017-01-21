using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public string PlayerNumber;
    public int maxHealth;
    public int currentHealth;
    

    public GameObject mainCamera;
    public GameObject missilePrefab;
    

    public Transform playerCenter;
    public float gravity;
    public float jumpSpeed;

    public Transform missileSpawnLocation;
    public Transform VIPHoldLocation;

    public float speed;
    public float airSpeedModifier;
    public float missileSpeed;

    public float FireTime;

    float angle;
    float FireTimer;


    public Vector3 moveDirection = Vector3.zero;

    CharacterController controller;
    

    public enum State { NoMovement, GroundedMovement, Jumping }
    

    public State movementState;
    //public CostumeEnum equippedCostume;

    // Singleton Pattern


    void Awake()
    {

    }


    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();

        ChangeMovementState(State.GroundedMovement);

        currentHealth = maxHealth;

        //equippedCostume = CostumeEnum.standard;

        Physics.IgnoreLayerCollision(8, gameObject.layer);
        //bIsNearTree = false;

        //Physics.IgnoreLayerCollision(12, gameObject.layer);

        //DashTimer = DashTime;
        //DashCooldownTimer = DashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //cowboyAnimator.SetBool("Attacking", false);
        
        ControlUpdate();
        PowerUpdate();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Level1");
        }


        FireTimer -= Time.deltaTime;

    }


    //Controls and Movement Functions

    public void ChangeMovementState(State state)
    {
        movementState = state;
    }

    void ControlUpdate()
    {
        // Normal Movement (Grounded state)
        if(!controller.isGrounded)
        {
            ChangeMovementState(State.Jumping);
        }


        if (movementState == State.NoMovement)
        {

        }
        else if (movementState == State.GroundedMovement)
        {
            HorizontalMoveControl();
            AimControl();
            //moveDirection.y = 0;
            if (Input.GetButtonDown("Jump" + PlayerNumber))
            {
                Debug.Log("Jump" + PlayerNumber);
                moveDirection.y = jumpSpeed;
                ChangeMovementState(State.Jumping);
            }

            controller.Move(moveDirection * Time.deltaTime);

            playerCenter.transform.rotation = Quaternion.Euler(0, angle, 0);

            

        }
        else if (movementState == State.Jumping)
        {
            HorizontalMoveControl();
            AimControl();
            controller.Move(moveDirection * Time.deltaTime * airSpeedModifier);
            moveDirection.y -= (gravity) * Time.deltaTime;
            playerCenter.transform.rotation = Quaternion.Euler(0, angle, 0);

            if(controller.isGrounded && moveDirection.y < 0)
            {
                ChangeMovementState(State.GroundedMovement);
            }
        }

            

    }

    //void CameraControl()
    //{
    //	lookDirectionH = new Vector3(0, Input.GetAxis("Horizontal2"), 0);
    //	transform.Rotate(lookDirectionH * lookSpeedH);


    //	lookDirectionV = new Vector3(Input.GetAxis("Vertical2"), 0, 0);
    //	cameraAxisLocation.Rotate(lookDirectionV * lookSpeedV);
    //	//Debug.Log(cameraAxisLocation.rotation.eulerAngles.x);

    //	if (cameraAxisLocation.rotation.eulerAngles.x > 270)
    //	{
    //		cameraAxisLocation.localEulerAngles = new Vector3(Mathf.Clamp(cameraAxisLocation.rotation.eulerAngles.x, 280, 400), cameraAxisLocation.rotation.y, cameraAxisLocation.rotation.z);
    //	}
    //	else
    //	{
    //		cameraAxisLocation.localEulerAngles = new Vector3(Mathf.Clamp(cameraAxisLocation.rotation.eulerAngles.x, -10, 80), cameraAxisLocation.rotation.y, cameraAxisLocation.rotation.z);
    //	}




    //	/*
    //       lookDirectionV = Input.GetAxis("Vertical2") * lookSpeedV;
    //       cameraRotationAxis = rightSide.position - transform.position;
    //       mainCamera.transform.RotateAround(transform.position, cameraRotationAxis, lookDirectionV);
    //        */

    //}

    void HorizontalMoveControl()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal" + PlayerNumber), moveDirection.y, Input.GetAxis("Vertical" + PlayerNumber));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.x *= speed;
        moveDirection.z *= speed;
    }

    void AimControl()
    {
        if (Mathf.Abs(Input.GetAxis("RHorizontal" + PlayerNumber)) > 0.01f || Mathf.Abs(Input.GetAxis("RVertical" + PlayerNumber)) > 0.01f)
        {
            angle = Mathf.Atan2(Input.GetAxis("RHorizontal" + PlayerNumber), Input.GetAxis("RVertical" + PlayerNumber)) * Mathf.Rad2Deg;
            Fireball();
        }



    }

 

    //void AscendMoveControl()
    //{
    //	moveDirection.y = ascendSpeed;
    //}

    //void DescendMoveControl()
    //{
    //	moveDirection.y = -descendSpeed;
    //}

    // Trigger Handling

    //void OnTriggerEnter(Collider other)
    //{
    //	if (other.GetComponent<Trigger>())
    //	{
    //		other.GetComponent<Trigger>().OnActivate();
    //	}
    //}

    //void OnTriggerExit(Collider other)
    //{
    //	if (other.GetComponent<Trigger>())
    //	{
    //		other.GetComponent<Trigger>().OnDeactivate();
    //	}
    //}

    // Powers

    void PowerUpdate()
    {
        if (Input.GetButtonDown("Fire" + PlayerNumber))
        {
            
        }

        //if (Input.GetButtonDown("Wind"))
        //{
        //	tempWind = WindBlow(windForce, false);
        //}

        //if (Input.GetButtonUp("Wind"))
        //{
        //	UnWindBlow(tempWind);
        //}

        //if (Input.GetButtonDown("Dash"))
        //{
        //	StartDash();
        //}

        ////Dash Timer
        //if (DashTimer < DashTime)
        //{
        //	Dash();
        //	DashTimer += Time.deltaTime;


        //	if (DashTimer > DashTime)
        //	{
        //		EndDash();
        //	}

        //}

        //if (DashCooldownTimer <= DashCooldown)
        //	DashCooldownTimer += Time.deltaTime;



    }

    void Fireball()
    {
        if(FireTimer <= 0)
        {
            GameObject go = (GameObject)Instantiate(missilePrefab, missileSpawnLocation.position, missileSpawnLocation.rotation);

            Vector3 bulletdirection = new Vector3(moveDirection.x, 0, moveDirection.z) * .5f;

            go.GetComponent<Rigidbody>().velocity = (missileSpawnLocation.transform.forward) * missileSpeed + bulletdirection;
            FireTimer = FireTime;
        }
        
    }

    public void Grab(Pickupable p)
    {
        p.GetComponent<Rigidbody>().transform.position = VIPHoldLocation.position;
        p.gameObject.transform.SetParent(this.gameObject.transform);

    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<EnemyBase>())
        //{
        //    Damage(other.GetComponent<EnemyBase>().damage);
        //    Destroy(other.gameObject);

        //    iTween.PunchPosition(mainCamera, new Vector3(0.0f, cameraPunchStrength, 0.0f), cameraPunchTime);
        //}
        //else if (other.GetComponent<Costume>())
        //{
        //    ChangeCostume();
        //    Destroy(other.gameObject);
        //}
        //Destroy(other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        //if (other.GetComponent<AmmoStation>() && other.GetComponent<AmmoStation>().active)
        //{
        //    other.GetComponent<AmmoStation>().Deactivate();
        //    ammo += other.GetComponent<AmmoStation>().ammoYield;
        //}
        //Destroy(other.gameObject);
    }

    public void Damage(int dmg)
    {
        currentHealth -= dmg;
    }

    //void Tether()
    //{
    //	ChangeMovementState(TetheredMovement);
    //}

    //void UnTether()
    //{
    //	ChangeMovementState(GroundedMovement);
    //}

    //GameObject WindBlow(float force, bool Dash)
    //{
    //	GameObject go = (GameObject)Instantiate(windPrefab, windSpawnLocation.position, mainCamera.transform.rotation);



    //	go.GetComponentInChildren<Wind>().windForce = force;
    //	if (!Dash)
    //	{
    //		go.transform.parent = mainCamera.transform;
    //		go.transform.Rotate(new Vector3(350, 0, 0));
    //	}
    //	else
    //	{
    //		go.transform.localEulerAngles = new Vector3(0, go.transform.localEulerAngles.y, go.transform.localEulerAngles.z);
    //	}

    //	return go;
    //}

    //void UnWindBlow(GameObject go)
    //{
    //	Destroy(go.gameObject);
    //}

    // To Do:

    //void StartDash()
    //{
    //	if (DashCooldownTimer > DashCooldown)
    //	{
    //		DashTimer = 0;
    //		dashDirection = transform.forward;
    //		dashDirection *= dashSpeed;
    //		tempWindDash = WindBlow(dashWindForce, true);
    //		ChangeMovementState(DashMovement);
    //		DashCooldownTimer = 0;
    //	}
    //}

    //void Dash()
    //{
    //	controller.Move(dashDirection * Time.deltaTime);

    //}

    //void EndDash()
    //{
    //	ChangeMovementState(GroundedMovement);
    //	UnWindBlow(tempWindDash);
    //}
}

