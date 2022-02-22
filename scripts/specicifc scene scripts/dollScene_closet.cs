using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dollScene_closet : MonoBehaviour
{
    //for level 1 doll scene ONLY

    public GameObject hand_sprite1;
    public Text description_text;
    public string text_shown;
    public string description;
    public KeyCode inspectKey = KeyCode.UpArrow;
    public bool canInspect;

    public float letterPause = 0.1f;

    public GameObject daughterDoll;
    int counter = 0;

    public Interacte_collect collectScript;
    public bool gotDoll;

    public Animator closerDoorAnim;
    AudioSource aud;

    void Start()
    {
        hand_sprite1.SetActive(false);
        daughterDoll.SetActive(false); //doll will have collection script on it

        text_shown = " ";
        canInspect = false;

        gotDoll = false;

        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canInspect)
        {
            if (Input.GetKeyDown(inspectKey))
            {
                StartCoroutine(TypeSentence(description));

                //show closet open animation && Doll there
                closerDoorAnim.SetTrigger("open");
                aud.Play();
                //can obtain doll
                daughterDoll.SetActive(true);

                counter += 1;
            }

        }
        else if (canInspect == false)
        {

           hand_sprite1.SetActive(false);

        }

        if(counter >= 1)
        {
            hand_sprite1.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
        }

        if (collectScript.collectedItem == true)
        {
            //can do the puzzle
            Debug.Log("got doll");
            gotDoll = true;
        }

  
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hand_sprite1.SetActive(true);
            canInspect = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            description_text.text = text_shown;
            canInspect = false;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        string[] array = sentence.Split(' ');
        description_text.text = array[0];
        for (int i = 1; i < array.Length; ++i)
        {
            yield return new WaitForSeconds(letterPause);
            description_text.text += " " + array[i];
        }
    }
}
