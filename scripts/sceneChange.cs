using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneChange : MonoBehaviour
{
    //change scene w/ fade Out
    //Put on a triggered Collider

    //public Animator anim;

    public string sceneName;

    //public AudioSource aud;
    //float startVolume = 1;

    bool startFade = false;

    //image on canvas
    public Image black_image;
    public AudioSource audioSource;

    void Start()
    {

    }


    void Update()
    {
        if (startFade)
        {
            //aud.volume -= startVolume * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //anim.SetTrigger("fadeOut");
            StartCoroutine(changeScene());

            //if (aud.isPlaying == true)
            //{
            //    startFade = true;
            //}
        }
    }

    public IEnumerator changeScene()
    {
        //anim.SetTrigger("fadeOut");
        StartCoroutine(FadeTo(1f, 2f));

        //fade OUt sound
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 2f, 0f));

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


    //for tutorial/playtest & clicking with button
    public void SceneClick()
    {
        SceneManager.LoadScene(sceneName);
    }

}
