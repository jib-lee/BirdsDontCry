using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact_Hand : MonoBehaviour
{
    //for any interactable with need to open canvas

    public GameObject hand_sprite;
    //public GameObject text_sprite; //if person has to talk to

    public GameObject img_cavas;

    public bool canvasOpen;

    bool canInspect;
    public KeyCode inspectKey = KeyCode.UpArrow;
    public KeyCode closeImgKey = KeyCode.DownArrow;

    public float letterPause = 0.1f;
    public Text description_text; //img_canvas' Text
    public string text_shown;
    public string description;
    public AudioSource collectSoundSource;

    void Start()
    {
        canInspect = false;
        img_cavas.SetActive(false);
        canvasOpen = false;
        //text_sprite.SetActive(false);

        text_shown = " ";
    }

    
    void Update()
    {
        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                img_cavas.SetActive(true);
                canvasOpen = true;
                collectSoundSource.Play();
                StartCoroutine(TypeSentence(description));
            }

        }
        else if (canInspect == false)
        {
            hand_sprite.SetActive(false);
            //text_sprite.SetActive(false);

        }

        if(canvasOpen)
        {
            if (Input.GetKeyDown(closeImgKey))
            {
                img_cavas.SetActive(false);
                canvasOpen = false;
                description_text.text = " ";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hand_sprite.SetActive(true);
            //text_sprite.SetActive(true);
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
        //wait for canvas to open before typing
        yield return new WaitForSeconds(0.1f);

        string[] array = sentence.Split(' ');
        description_text.text = array[0];
        for (int i = 1; i < array.Length; ++i)
        {
            yield return new WaitForSeconds(letterPause);
            description_text.text += " " + array[i];
        }
    }
}
