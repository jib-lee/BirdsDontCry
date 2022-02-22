using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interact_Look : MonoBehaviour
{
    //for interactable to have text explanation

    public GameObject look_sprite;
    public Text description_text;
    public string text_shown;
    public string description;
  

    public KeyCode inspectKey = KeyCode.UpArrow;
    public bool canInspect;

    public float letterPause = 0.1f;

    void Start()
    {
        look_sprite.SetActive(false);
        text_shown = " ";
        canInspect = false;
    }

    void Update()
    {
        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                //text_shown = description;
                StartCoroutine(TypeSentence(description));
                //description_text.text = description;

            }

        } else if (canInspect == false)
        {
            
            look_sprite.SetActive(false);
 
        }

 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            look_sprite.SetActive(true);
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

    //fade out text (if needed)
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {

        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

    }
}
