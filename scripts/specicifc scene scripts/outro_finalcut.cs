using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class outro_finalcut : MonoBehaviour
{
    public string sceneName;
    bool startFade = false;
    //image on canvas
    public Image black_image;
    public AudioSource audioSource;
    public AudioSource birdSound;
    public AudioSource hospital_beep_sound;

    public GameObject player;
    public GameObject flowchart;

    void Start()
    {
        flowchart.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public IEnumerator changeScene()
    {
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 2.5f, 0.7f));
        StartCoroutine(FadeAudioSource.StartFade(hospital_beep_sound, 4f, 0.1f));
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 4.5f, 0f));
        StartCoroutine(FadeAudioSource.StartFade(birdSound, 4.5f, 0f));
        StartCoroutine(FadeTo(1f, 5f));
        yield return new WaitForSeconds(7f);
        StartCoroutine(FadeAudioSource.StartFade(hospital_beep_sound, 2f, 0f));
        yield return new WaitForSeconds(2.5f);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(changeScene());
        
            player.GetComponent<playerController>().walkSpeed = 0;
            flowchart.SetActive(true);
        }
    }
}
