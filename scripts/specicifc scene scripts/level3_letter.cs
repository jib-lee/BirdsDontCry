using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level3_letter : MonoBehaviour
{
    public GameObject hand_sprite;
    public GameObject img_cavas;

    public bool canvasOpen;

    bool canInspect;
    public KeyCode inspectKey = KeyCode.UpArrow;

    public float letterPause = 0.1f;
    public Text description_text; //img_canvas' Text
    public string text_shown;
    public string description;

    public GameObject player;

    //public Animator letterCanvasAnim;
    public GameObject killGroup;
    public GameObject canvasBG;
    public GameObject letter;

    public AudioSource collectSound;

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
                collectSound.Play();
                img_cavas.SetActive(true);
                canvasOpen = true;
                StartCoroutine(TypeSentence(description));
            }

        }
        else if (canInspect == false)
        {
            hand_sprite.SetActive(false);
            //text_sprite.SetActive(false);

        }

        if (canvasOpen)
        {
            //does animation of kill on the letter
            
        }

        if (canvasBG.activeSelf == false)
        {
            hand_sprite.SetActive(false);
            letter.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
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
