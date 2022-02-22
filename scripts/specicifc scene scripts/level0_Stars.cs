using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level0_Stars : MonoBehaviour
{
    public Interact_Look lookScript;

    public GameObject[] stars;

    public BoxCollider2D starTrigCol;
    public GameObject eye_sprite;

    public Shake cameraShakeScript;
    int count = 0;
    public AudioSource aud;

    void Start()
    {
        
    }
   
    void Update()
    {
        if (lookScript.canInspect == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
              
                if (count < 2)
                {
                    StartCoroutine(fallingStars());
                    
                }
                  

            }
        }

    }


    IEnumerator fallingStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            aud.Play();
            //shake all the time?
           cameraShakeScript.ScreenShake(0.03f, 0.75f);
            
            stars[i].GetComponent<Rigidbody2D>().gravityScale = 1;
            yield return new WaitForSeconds(1f);
            count += 1;
        }
    }

 

}
