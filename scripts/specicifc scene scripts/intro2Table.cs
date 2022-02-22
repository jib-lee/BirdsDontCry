using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class intro2Table : MonoBehaviour
{
    //sits at table to start next scene

    public GameObject hand_sprite;

    public KeyCode inspectKey = KeyCode.UpArrow;
    public bool canInspect;


    public string sceneName;
    public Image black_image;


    void Start()
    {
        canInspect = false;
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
            canInspect = false;
        }
    }

    public IEnumerator changeScene()
    {
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
