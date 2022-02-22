using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outro_Dad : MonoBehaviour
{
    public GameObject dad;
    public GameObject player;
    public Animator playerAnim;

    public GameObject bird;

    float dist;
    public float checkDist;

    void Start()
    {
        bird.SetActive(false);
        player.GetComponent<playerController>().walkSpeed = 0f;
        StartCoroutine(fadeDad());

        
    }

    // Update is called once per frame
    void Update()
    {

         
        
    }

    IEnumerator fadeDad()
    {
        
        yield return new WaitForSeconds(3f);
        player.GetComponent<playerController>().walkSpeed = 3f;

        bird.SetActive(true);
    }
}
