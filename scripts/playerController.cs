using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Values to tweak for the movement. 
    public float walkSpeed = 3f;

    // We walk by using AddForce, so we have to specify how fast we accelerate
    public float walkAccelerationForce = 15f;

    // A constant for damping our movement when we stop walking or jumping.
    public float dampingK = 10f;

    // Controls
    public KeyCode walkRightKey = KeyCode.RightArrow;
    public KeyCode walkLeftKey = KeyCode.LeftArrow;
    public KeyCode inspectKey = KeyCode.UpArrow;
    public KeyCode quitKey = KeyCode.Escape;

    protected Rigidbody2D _body;

    public bool canMove = true;

    float someScale;
    public GameObject playerBodyGroup; //flipping only the sprite group not the dialoguebox
    public Animator anim;
    private Rigidbody2D rb;

    [Tooltip ("-1 is facing right and 1 is left")]
    public int direction;

    AudioSource aud;

    public GameObject quitCanvas;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        //no cursor during gameplay
        Cursor.visible = false;

        someScale = playerBodyGroup.transform.localScale.x;
        playerBodyGroup.transform.localScale = new Vector2(someScale * direction, playerBodyGroup.transform.localScale.y);
        rb = GetComponent<Rigidbody2D>();

        aud = GetComponent<AudioSource>();

        quitCanvas.SetActive(false);
    }


    void Update()
    {
        //quit game 
        if (Input.GetKeyDown(quitKey))
        {
            quitCanvas.SetActive(true);
          
        }

        if (quitCanvas.activeSelf == true)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                Application.Quit(); 

            } else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                quitCanvas.SetActive(false);
          
            }
        }

    }

    // Meanwhile, use fixed update to check for continuous movement like walking. 
    void FixedUpdate()
    {
        if (canMove)
        {
            bool walkingRight = false;
            if (Input.GetKey(walkRightKey) && _body.velocity.x < walkSpeed)
            {
                _body.AddForce(walkAccelerationForce * Vector2.right);
                walkingRight = true;
            }
            bool walkingLeft = false;
            if (Input.GetKey(walkLeftKey) && _body.velocity.x > -walkSpeed)
            {
                _body.AddForce(-walkAccelerationForce * Vector2.right);
                walkingLeft = true;
            }
            // If we're moving too fast or we've stopped pressing move keys, apply a damping force
            if (Mathf.Abs(_body.velocity.x) > walkSpeed || (!walkingLeft && !walkingRight))
            {
                _body.AddForce(-dampingK * _body.velocity.x * Vector2.right);
            }

            //flip
            if (walkingLeft)
            {

                playerBodyGroup.transform.localScale = new Vector2(someScale, playerBodyGroup.transform.localScale.y);

            }
            else if (walkingRight)
            {
                playerBodyGroup.transform.localScale = new Vector2(-someScale, playerBodyGroup.transform.localScale.y);

            }

            if(walkingLeft || walkingRight && rb.velocity.x != 0)
            {
                anim.SetBool("walking", true);
           
            } else if (rb.velocity.x == 0)
            {
                anim.SetBool("walking", false);
                
            }
        }
     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("level5Ground")) { 
            Debug.Log("on ground");
            anim.SetBool("gotThrown", false);
        }
    }

}
