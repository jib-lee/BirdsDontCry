using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class level0_BedRoomdoor : MonoBehaviour
{
    //for door opening w/ hand sprite
    public GameObject handSprite;
    bool canInspect;
    public KeyCode inspectKey = KeyCode.UpArrow;

    public string sceneName;
    bool startFade = false;
    //image on canvas
    public Image black_image;
    public AudioSource audioSource;

    void Start()
    {
        handSprite.SetActive(false);
    }

    void Update()
    {
        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                StartCoroutine(changeScene());
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
            handSprite.SetActive(true);
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

    public IEnumerator changeScene()
    {
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 2f, 0f));
        StartCoroutine(FadeTo(1f, 2f));
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeTo(float aValue, float aTime)
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
