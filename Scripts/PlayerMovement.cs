using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    [SerializeField] float movespeed = 10f;
    bool facingRight = true;
    float inputHorizontal;
    float inputVertical;
    public Rigidbody rb;
    public float jumpSpeed;
    PhotonView view;


    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadious;
    public LayerMask whatISGround;
    private int extraJumps;
    public int extraJumpsValue;

    public GameObject followCamera;

    private Touch touch;
    private float touchspeed = 0.1f;
    float touchspeed1 = 10f;
    // Start is called before the first frame update


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
        extraJumps = extraJumpsValue;

    }

    // Update is called once per frame
    void Update()
    {

        //jump();

        if (view.IsMine)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);

            followCamera.SetActive(true);
            TouchMovement();
            //    inputHorizontal = Input.GetAxisRaw("Horizontal");
            //    inputVertical = Input.GetAxis("Vertical");
            //    // transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime * movespeed, yValue, Input.GetAxis("Horizontal") * Time.deltaTime * movespeed);

            //    Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //    transform.position += input.normalized * movespeed * Time.deltaTime;

            //    if (input == Vector3.zero)
            //    {
            //        anim.SetBool("isRunning", false);
            //    }
            //    else
            //    {
            //        anim.SetBool("isRunning", true);
            //    }

            //    if (Input.GetKey(KeyCode.Z))
            //    {
            //        rb.AddForce(Vector3.up * jumpSpeed);
            //    }

        }
        else
        {
            followCamera.SetActive(false);
        }

    }


    public void TouchMovement()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * touchspeed, transform.position.y , transform.position.z + touch.deltaPosition.y * touchspeed);
                anim.SetBool("isRunning", true);
            }


        }
        else
        {
            anim.SetBool("isRunning", false);

        }
    }



    public void jump()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKey(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector3.up * jumpSpeed;
            extraJumps--;
        }
        if (Input.GetKey(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector3.up * jumpSpeed;
        }
    }
}
