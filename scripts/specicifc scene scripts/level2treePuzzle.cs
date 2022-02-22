using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2treePuzzle : MonoBehaviour
{
    //put on the tree for the jacket to drop

    public GameObject hand_sprite;

    bool canInspect;
    public KeyCode inspectKey = KeyCode.UpArrow;

    public GameObject jacket;
    public BoxCollider2D jacketInspectCollider;

    public int pushCounter;

    AudioSource aud;

    public Animator branchAnim;
    public Animator hangingBodAnim;
    public AudioSource hangbodAud;

    void Start()
    {
        canInspect = false;
        pushCounter = 0;

        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                //play tree shake anim
                aud.Play();
                branchAnim.SetTrigger("shake");
                pushCounter += 1;
            }

        }
        else if (canInspect == false)
        {
            hand_sprite.SetActive(false);

        }

        if (pushCounter >= 4)
        {
            jacket.GetComponent<Rigidbody2D>().gravityScale = 1;
            jacketInspectCollider.enabled = false;
        }

        if (pushCounter == 6)
        {
            hangingBodAnim.SetTrigger("fall");
            if (counter == 0)
            {
                hangbodAud.Play();
                counter++;
            }
        }
    }

    int counter = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hand_sprite.SetActive(true);
            canInspect = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInspect = false;
        }
    }
}
