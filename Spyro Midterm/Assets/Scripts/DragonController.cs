using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{

    public GameObject Camera;

    [SerializeField]
    Transform groundDetectCenterPoint;

    [SerializeField]
    float GroundDetectRadius = 0.2f;

    [SerializeField]
    LayerMask CountsAsGround = 8;

    [SerializeField]
    public float Accel;

    [SerializeField]
    public float jumpStrength = .05f;

    Rigidbody2D myRigidThing;

    private bool isOnGround;

    bool facingRight = true;

    Animator Anim1;

    [SerializeField]
    float GlideForce;



    // Use this for initialization
    void Start ()
    {
        myRigidThing = GetComponent<Rigidbody2D>();
        Anim1 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateIsOnGround();
        //HandleMovement();
        //HandleJump();

        float VerticalInput = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            myRigidThing.velocity = new Vector2(myRigidThing.velocity.x, jumpStrength);
            isOnGround = false;
        }

        if (Input.GetButtonDown("Jump") && !isOnGround)
        {
            if (facingRight)
                myRigidThing.AddForce(Vector2.right * GlideForce );

            if (!facingRight)
                myRigidThing.AddForce(Vector2.left * GlideForce);

            Anim1.SetBool("Gliding", true);

            myRigidThing.drag = 20;
            myRigidThing.gravityScale = .5f;

        }


        float HorizontalInput = Input.GetAxis("Horizontal");
        Anim1.SetFloat("Speed", Mathf.Abs(HorizontalInput));
        myRigidThing.velocity = new Vector2(Accel * HorizontalInput, myRigidThing.velocity.y);


        if (HorizontalInput > 0 && !facingRight)
            Flip();
        else if (HorizontalInput < 0 && facingRight)
            Flip();
    }

    void UpdateIsOnGround()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundDetectCenterPoint.position, GroundDetectRadius, CountsAsGround);

        isOnGround = groundObjects.Length > 0;
        Anim1.SetBool("Gliding", false);
        myRigidThing.drag = 3;
        myRigidThing.gravityScale = 1;

    }

    //private void HandleMovement()
    //{

    //    float HorizontalInput = Input.GetAxis("Horizontal");
    //    Anim1.SetFloat("Speed", Mathf.Abs(HorizontalInput));
    //    myRigidThing.velocity = new Vector2(Accel * HorizontalInput, myRigidThing.velocity.y);
        

    //    if (HorizontalInput > 0 && !facingRight)
    //        Flip();
    //    else if (HorizontalInput < 0 && facingRight)
    //        Flip();
    //}

    //private void HandleJump()
    //{
    //    float VerticalInput = Input.GetAxis("Vertical");
    //    if (Input.GetButtonDown("Jump") && isOnGround)
    //    {
    //        myRigidThing.velocity = new Vector2(myRigidThing.velocity.x, jumpStrength);
    //        isOnGround = false;
    //    }


    //    if (Input.GetButtonDown("Jump") && !isOnGround)
    //    {
    //        if (facingRight)
    //            myRigidThing.AddForce(Vector2.right * GlideForce);

    //        if(!facingRight)
    //            myRigidThing.AddForce(Vector2.left * GlideForce);
    //        //myRigidThing.AddForce(GlideForce, myRigidThing.velocity.y);

    //        Anim1.SetBool("Gliding", true);

    //        myRigidThing.drag = 20;

    //    }

    //}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


   
}
