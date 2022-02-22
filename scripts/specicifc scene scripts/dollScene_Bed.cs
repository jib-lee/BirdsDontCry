using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dollScene_Bed : MonoBehaviour
{
    public GameObject Dolls;
    public GameObject handSprite;
    public GameObject daughterDoll;
    public bool dollPlaced;

    public dollScene_closet closetScript;

    bool canInspect;
    public KeyCode inspectKey = KeyCode.UpArrow;

    public AudioSource aud;

    public Animator dollsHeadAnim;
    public AudioSource ripSound;
    public AudioSource thumpsound;
    public AudioSource creepyAttacksound;

    void Start()
    {
        handSprite.SetActive(false);
        daughterDoll.SetActive(false);
        dollPlaced = false;
    }

    int counter = 0;
    void Update()
    {
        if (closetScript.gotDoll == true)
        {
            Dolls.GetComponent<Interact_Look>().look_sprite.SetActive(false);
            Dolls.GetComponent<Interact_Look>().description = " ";
        }

        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                //solve puzzle
                //does animation of putting in doll?
                if (!dollPlaced)
                {
                    aud.Play();
                }
                handSprite.SetActive(false);
                daughterDoll.SetActive(true);
                dollPlaced = true;

                //then enemy will comes in
                if (counter == 0)
                {
                    StartCoroutine(DollsHeadFall()); ;
                }

            }

        }
        else if (canInspect == false)
        {

            handSprite.SetActive(false);

        }

  
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (closetScript.gotDoll == true && dollPlaced == false)
            {
                handSprite.SetActive(true);
                canInspect = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (closetScript.gotDoll == true)
            {
                canInspect = false;
            }
        }
    }

    IEnumerator DollsHeadFall()
    {
        counter++;
        yield return new WaitForSeconds(2f);
        dollsHeadAnim.SetBool("start", true);
        ripSound.Play();
        thumpsound.PlayDelayed(1.85f);
        creepyAttacksound.PlayDelayed(1.2f);

    }
}
