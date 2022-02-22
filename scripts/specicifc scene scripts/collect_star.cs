using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect_star : MonoBehaviour
{
    //BEDROOM ONLY

    public GameObject hand_sprite;
    bool canCollect;
    public KeyCode inspectKey = KeyCode.UpArrow;
    int counter = 0;
    public GameObject collectible;
    public level0_Stars lvl0starsscript;
    public AudioSource aud;

    void Start()
    {
        canCollect = false;
        hand_sprite.SetActive(false);
    }

    
    void Update()
    {
        if (canCollect)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                collectible.GetComponent<SpriteRenderer>().enabled = false;
                collectible.GetComponent<BoxCollider2D>().enabled = false;
                counter += 1;

                aud.Play();
            }

        }
        else if (canCollect == false)
        {
            hand_sprite.SetActive(false);

        }

        if (counter >= 1)
        {
            hand_sprite.SetActive(false);
            collectible.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hand_sprite.SetActive(true);
            canCollect = true;
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canCollect = false;
        }
    }
}
