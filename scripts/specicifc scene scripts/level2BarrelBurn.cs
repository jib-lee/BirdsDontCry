using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2BarrelBurn : MonoBehaviour
{
    public GameObject hand_sprite;
    bool canInspect;

    public KeyCode inspectKey = KeyCode.UpArrow;

    public level2shirt shirtMatch;

    public GameObject pileTrigger;
    public GameObject pile;
    bool destroyedPile;

    public Animator burnAnim;
    public AudioSource burnSound;

    void Start()
    {
        canInspect = false;
        destroyedPile = false;
    }


    int counter = 0;
    void Update()
    {
        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                //Burn the frames & got match
                pileTrigger.SetActive(false);
                destroyedPile = true;
                if (counter == 0)
                {
                    StartCoroutine(burnPile());
                }
            }

        }
        else if (canInspect == false)
        {
            hand_sprite.SetActive(false);

        }

        if(destroyedPile == true)
        {
            hand_sprite.SetActive(false);
        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (shirtMatch.gotMatch == true)
            {

                hand_sprite.SetActive(true);
                //text_sprite.SetActive(true);
                canInspect = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInspect = false;
        }
    }

    IEnumerator burnPile()
    {
        burnSound.Play();
        counter++;
        burnAnim.SetTrigger("burn");
        yield return new WaitForSeconds(8f);
        pile.SetActive(false);
    }
}
