using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacte_collect : MonoBehaviour
{
    //for hand --> collect the object and destroy the sprite

    //can be used as base for other puzzles that use collection if those are more complicated
    //PUT THIS ON AN EMPTY GAMEOBJECT WITH A COLLIDER TRIGGER

    public GameObject hand_sprite;
    public GameObject img_cavas;

    public bool canvasOpen;

    bool canInspect;
    public KeyCode inspectKey = KeyCode.UpArrow;
    public KeyCode closeImgKey = KeyCode.DownArrow;

    public float letterPause = 0.1f;
    public Text description_text; //img_canvas' Text
    public string text_shown;
    public string description;

    public GameObject collectible;
    private SpriteRenderer collectibleSprite;

    int counter = 0;

    public bool collectedItem;

    public AudioSource aud;

    void Start()
    {
        canInspect = false;
        img_cavas.SetActive(false);
        canvasOpen = false;

        text_shown = " ";

        collectibleSprite = collectible.GetComponent<SpriteRenderer>();

        collectedItem = false;
    }


    void Update()
    {
        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey) && counter < 1)
            {
                img_cavas.SetActive(true);
                canvasOpen = true;
                StartCoroutine(TypeSentence(description));
                aud.Play();
                //collectibleSprite.enabled = false; // turn off the sprite since its collected
                collectible.SetActive(false);
                counter += 1;
                collectedItem = true;
            }

        }
        else if (canInspect == false)
        {
            hand_sprite.SetActive(false);

        }

        if (canvasOpen)
        {
            if (Input.GetKeyDown(closeImgKey))
            {
                img_cavas.SetActive(false);
                canvasOpen = false;
                description_text.text = " ";
                
            }
        }

        if (counter >= 1)
        {
            hand_sprite.SetActive(false);
        }
    }

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
