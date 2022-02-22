using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level2shirt : MonoBehaviour
{
    public GameObject hand_sprite;
    public GameObject eye_sprite_2ndTime;
    //public GameObject text_sprite; //if person has to talk to

    public GameObject img_canvas1;
    public GameObject img_canvas2;

    public bool canvasOpen1;
    public bool canvasOpen2;

    bool canInspect;
    public KeyCode inspectKey = KeyCode.UpArrow;
    public KeyCode closeImgKey = KeyCode.DownArrow;

    public float letterPause = 0.1f;
    public Text description_text1; //img_canvas' Text
    public Text description_text2; //sencond canvas's
    public string text_shown;
    public string description1;
    public string description2;

    //look at stuff in jacket
    public GameObject eye_img;
    public bool foundLetter;
    public bool gotMatch;

    public Text normalCanvasText;
    public string description3;

    void Start()
    {
        canInspect = false;
        img_canvas1.SetActive(false);
        canvasOpen1 = false;
        //text_sprite.SetActive(false);

        text_shown = " ";

        eye_img.SetActive(false);
        foundLetter = false;
        img_canvas2.SetActive(false);
        canvasOpen2 = false;
        gotMatch = false;

        eye_sprite_2ndTime.SetActive(false);
    }

    public AudioSource collectSound;
    
    void Update()
    {
        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey) && !gotMatch)
            {
                collectSound.Play();
                img_canvas1.SetActive(true);
                canvasOpen1 = true;
                StartCoroutine(TypeSentence(description_text1, description1));

                StartCoroutine(eyeInspect());
            }

        }
        else if (canInspect == false)
        {
            hand_sprite.SetActive(false);
            //text_sprite.SetActive(false);

        }

        if (foundLetter && !gotMatch)
        {
            if (Input.GetKeyDown(inspectKey))
            {
                collectSound.Play();
                img_canvas2.SetActive(true);
                canvasOpen2 = true;
                StartCoroutine(TypeSentence(description_text2, description2));

                img_canvas1.SetActive(false);
                canvasOpen1 = false;
            }
        }

        if (canvasOpen2)
        {
            if (Input.GetKeyDown(closeImgKey))
            {
                img_canvas2.SetActive(false);
                canvasOpen2 = false;
                description_text2.text = " ";

                gotMatch = true;
            }
        }

        if (gotMatch && canInspect)
        {
            hand_sprite.SetActive(false);

            if (Input.GetKeyDown(inspectKey))
            {
                StartCoroutine(TypeSentence(normalCanvasText, description3));
            }
        }
    }

    public AudioSource thumpsound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            thumpsound.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hand_sprite.SetActive(true);
            //text_sprite.SetActive(true);
            canInspect = true;

            if (gotMatch)
            {
                eye_sprite_2ndTime.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //description_text.text = text_shown;
            canInspect = false;

            if (gotMatch)
            {
                eye_sprite_2ndTime.SetActive(false);
                normalCanvasText.text = text_shown;
            }
        }

    }

    IEnumerator TypeSentence(Text t, string sentence)
    {
        //wait for canvas to open before typing
        yield return new WaitForSeconds(0.1f);

        string[] array = sentence.Split(' ');
        t.text = array[0];
        for (int i = 1; i < array.Length; ++i)
        {
            yield return new WaitForSeconds(letterPause);
            t.text += " " + array[i];
        }
    }

    IEnumerator eyeInspect()
    {
        yield return new WaitForSeconds(1.5f);
        eye_img.SetActive(true);
        foundLetter = true;
    }
}
