using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{

    public string sceneName;

    public Image black_image;
    public AudioSource audioSource;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(changeScene());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    public IEnumerator changeScene()
    {
        //anim.SetTrigger("fadeOut");
        StartCoroutine(FadeTo(1f, 3f));

        //fade OUt sound
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 3f, 0f));

        yield return new WaitForSeconds(4f);
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
