using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class stairs_light : MonoBehaviour
{
    //Put this triggered collider

    public GameObject player;

    public Light2D[] pointLights;
    float brightness;

    void Start()
    {

    }

    
    void Update()
    {

    }

    IEnumerator fadeInLight(Light2D pl ,float aValue, float aTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
      
            brightness = Mathf.Lerp(brightness, aValue, t);
            pl.intensity = brightness;

            yield return null;

        }
    }

    public AudioSource gecko;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //for (int i = 0; i < pointLights.Length; i++)
            //{
            //    StartCoroutine(fadeInLight(pointLights[i], 1f, 3f));

            //}
            StartCoroutine(LightingEffect());
            gecko.PlayDelayed(4f);
        }
    }

    IEnumerator LightingEffect()
    {
        for (int i = 0; i < pointLights.Length; i++)
        {
            StartCoroutine(fadeInLight(pointLights[i], 0.75f, 4f));

            yield return new WaitForSeconds(1f);

        }
    }
}
