using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeIn : MonoBehaviour
{
    //for fade from black --> clear
    //starting a scene, can be put on scenemanager empty game item

    public Image black_image;
    public float fadeDuration;

    void Start()
    {
        StartCoroutine(FadeTo(0f, fadeDuration));
    }

    // Update is called once per frame
    void Update()
    {
        
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
