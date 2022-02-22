using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flyer_sceneBlood : MonoBehaviour
{
    //FOR FLYER SCENE ONLY

    public GameObject hand_sprite;
    //public GameObject text_sprite; //if person has to talk to

    public GameObject img_cavas;

    public bool canvasOpen;

    bool canInspect;
    public KeyCode inspectKey = KeyCode.UpArrow;
    public KeyCode closeImgKey = KeyCode.DownArrow;

    public GameObject blood;
    //public GameObject posters;
    public GameObject person;
    BoxCollider2D personCollider;
    public BoxCollider2D posterCollider;

    public Text description_text; //img_canvas' Text
    public string description;
    public float letterPause = 0.1f;

    public AudioSource bloodsound;
    public AudioSource cicada;

    void Start()
    {
        canInspect = false;
        img_cavas.SetActive(false);
        canvasOpen = false;
        //text_sprite.SetActive(false);
        personCollider = GetComponent<BoxCollider2D>();
        blood.SetActive(false);

        //fade in cicada sound
        StartCoroutine(FadeAudioSource.StartFade(cicada, 2f, 0.6f));
    }


    void Update()
    {
        if (canInspect)
        {
            if (Input.GetKeyDown(closeImgKey))
            {
                //text_sprite.SetActive(false);
            }

            if (Input.GetKeyDown(inspectKey))
            {
                img_cavas.SetActive(true);
                StartCoroutine(TypeSentence(description));
                canvasOpen = true;
            }

        }
        else if (canInspect == false)
        {
            hand_sprite.SetActive(false);
            //text_sprite.SetActive(false);

        }

        if (canvasOpen)
        {
            //kill that man before canvas closes
            blood.SetActive(true);
            //posters.SetActive(false);
            person.SetActive(false);
            personCollider.enabled = false;
            posterCollider.enabled = false;

            if (Input.GetKeyDown(closeImgKey))
            {
                img_cavas.SetActive(false);
                canvasOpen = false;
                bloodsound.Play();
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
