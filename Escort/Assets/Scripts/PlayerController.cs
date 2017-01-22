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
    public GameObject grabBox;

    public Transform playerCenter;
    public float gravity;
    public float jumpSpeed;

    public Transform missileSpawnLocation;
    public Transform VIPHoldLocation;

    public float throwForce;

    public float speed;
    public float airSpeedModifier;
    public float missileSpeed;

    public float FireTime;
    public float GrabTime;

    float angle;
    float FireTimer;
    float GrabTimer;


    public Vector3 moveDirection = Vector3.zero;

    CharacterController controller;
    public Pickupable heldObject;

    public enum State { NoMovement, GroundedMovement, Jumping }

    bool holding;
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
        holding = false;

        Physics.IgnoreLayerCollision(8, gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {
        
        ControlUpdate();
        PowerUpdate();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Level1");
        }


        FireTimer -= Time.deltaTime;
        GrabTimer -= Time.deltaTime;

        if(GrabTimer <= 0)
        {
            grabBox.GetComponent<GrabBox>().isactive = false;
        }
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

    void PowerUpdate()
    {
        if (!holding && Input.GetButtonDown("Fire" + PlayerNumber))
        {
            grabBox.GetComponent<GrabBox>().SetActive(true);
            GrabTimer = GrabTime;
        }
        else if (Input.GetButtonDown("Fire" + PlayerNumber))
        {
            Chuck();
        }



    }

    void Fireball()
    {
        if(FireTimer <= 0 && !holding)
        {
            GameObject go = (GameObject)Instantiate(missilePrefab, missileSpawnLocation.position, missileSpawnLocation.rotation);

            Vector3 bulletdirection = new Vector3(moveDirection.x, 0, moveDirection.z) * .5f;

            go.GetComponent<Rigidbody>().velocity = (missileSpawnLocation.transform.forward) * missileSpeed + bulletdirection;
            FireTimer = FireTime;
        }
        
    }

    public void Grab(Pickupable p)
    {
        if (!p.held)
        {
            p.GetComponent<Rigidbody>().transform.position = VIPHoldLocation.position;
            p.gameObject.transform.SetParent(this.gameObject.transform);

            p.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.x, 0);

            p.GetComponent<Rigidbody>().isKinematic = true;
            p.held = true;
            heldObject = p;
            holding = true;
        }

    }

    public void Chuck()
    {
        Vector3 movedirection = new Vector3(moveDirection.x, 0, moveDirection.z) * .5f;

        heldObject.gameObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<Rigidbody>().AddForce(playerCenter.forward * throwForce + moveDirection);
        heldObject.held = false;
        holding = false;

        if(heldObject.gameObject.GetComponent<VIP>())
        {
            heldObject.gameObject.GetComponent<VIP>().GetUp();
        }

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

   
}

