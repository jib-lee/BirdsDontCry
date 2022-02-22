using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerBar : MonoBehaviour
{
    Image barImage;

    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();

        barImage.fillAmount = 0.2f;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
