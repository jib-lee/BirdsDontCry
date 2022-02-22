using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdBehavior : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject player;

    public float speed = 3f;
    public float bouceSpeed = 1f;
    public int distToPlayer;
    public float bounceDistance = 0.5f;

    bool startToWait;

    public int waitOffset;

    Vector2 objHeight;

    public AnimationCurve test;

    public GameObject house;
    public Vector3 perchPoint;

    bool canFly = true;

    SpriteRenderer sr;

    void Start()
    {
        startToWait = false;
        objHeight = this.transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (canFly)
        {

            if (!startToWait)
            {
                this.gameObject.transform.Translate(Vector2.right * Time.deltaTime * speed);
            }

            if (this.gameObject.transform.position.x > player.transform.position.x + waitOffset)
            {
                startToWait = true;
            }

            bool playerIsClose = Physics2D.OverlapCircle(transform.position, distToPlayer, playerLayer);

            if (startToWait == true)
            {
                if (playerIsClose)
                {

                    this.gameObject.transform.Translate(Vector2.right * Time.deltaTime * speed);
                    sr.flipX = false;

                }
                else
                {
                    Wait();
                    sr.flipX = true;

                }

            }
        } else if (!canFly)
        {
            speed = 0;
            Wait();
            //do perched anim
        }

        if (this.transform.position.x - house.transform.position.x >= -5.5)
        {
            canFly = false;
        }

        Debug.Log(this.transform.position.x - house.transform.position.x);
    }

    void Wait()
    {
        Vector2 newPos = transform.position;
        //newPos.y = objHeight.y + Mathf.SmoothStep(-bounceDistance, bounceDistance, Mathf.PingPong(Time.time * bouceSpeed, 1f));
        newPos.y = objHeight.y + test.Evaluate(Time.time * bouceSpeed);
        transform.position = newPos;
    }
}
