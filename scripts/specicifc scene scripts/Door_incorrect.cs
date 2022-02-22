using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door_incorrect : MonoBehaviour
{
    //put on the correct OR incoorect door w/ sceneName differing

    public GameObject eye_sprite;
    public GameObject hand_sprite;

    bool looked;
    bool canInspect;
    public GameObject img_cavas;
    public GameObject fisheye_filter;
    public bool canvasOpen;
    bool onDoor;

    public KeyCode inspectKey = KeyCode.UpArrow;
    public KeyCode closeImgKey = KeyCode.DownArrow;

    public string sceneName;

    //image on canvas for fade
    public Image black_image;

    //animation for eye opening & closing!

    public AudioSource doorcreak;
    public AudioSource BGM;

    void Start()
    {
        eye_sprite.SetActive(false);
        hand_sprite.SetActive(false);
        img_cavas.SetActive(false);
        fisheye_filter.SetActive(false);

        looked = false;
        canInspect = false;
        canvasOpen = false;
    }

  
    void Update()
    {
        if (canInspect && !looked)
        {
            if (Input.GetKeyDown(inspectKey))
            {
                img_cavas.SetActive(true);
                fisheye_filter.SetActive(true);
                canvasOpen = true;
            }
        }

        if (canvasOpen)
        {
            //have a bool to check the anim? play for 5 sec etc

            if (Input.GetKeyDown(closeImgKey))
            {
                img_cavas.SetActive(false);
                fisheye_filter.SetActive(false);
                canvasOpen = false;
                looked = true;
            }
        }

        if (looked)
        {
            eye_sprite.SetActive(false);

            if (onDoor)
            {

                hand_sprite.SetActive(true);

                if (Input.GetKeyDown(inspectKey))
                {
                    StartCoroutine(changeScene());
                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!looked)
            {
                eye_sprite.SetActive(true);
                canInspect = true;
            }

            if (looked)
            {
                hand_sprite.SetActive(true);
            }

            onDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInspect = false;
            eye_sprite.SetActive(false);
            hand_sprite.SetActive(false);

            onDoor = false;
        }
    }

    IEnumerator changeScene()
    {
        doorcreak.Play();
        StartCoroutine(FadeAudioSource.StartFade(BGM, 2f, 0f));
        StartCoroutine(FadeTo(1f, 2f));
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = black_image.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            black_image.color = newColor;
            yield return null;
        }
    }
}
