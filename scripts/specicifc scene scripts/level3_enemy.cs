using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3_enemy : MonoBehaviour
{
    public GameObject enemyStart;

    public resist_InAction resistScript;

    int speed = 1;

    public bool triggered;

    public Shake cameraShakeScript;
    public GameObject player;

    public GameObject killGroup;
    public GameObject letter_canvas;
    public GameObject enemyForFight;

    bool shook;

    public GameObject door_eyes;


    void Start()
    {
        enemyForFight.SetActive(false);

        triggered = false;
        StartCoroutine(enemyAtStart());

        shook = false;

        door_eyes.SetActive(false);
    }


    void Update()
    {
        //if opened letter
        if (killGroup.activeSelf == false && resistScript.startFight == false)
        {
            StartCoroutine(enemyIn(1.5f));

            if (shook == false)
            {
                StartCoroutine(isShook(5f));
            }

            door_eyes.SetActive(true);
        }
        //if havent opened letter
        else if (triggered && letter_canvas.activeSelf == false)
        {
            StartCoroutine(enemyIn(1.5f));

            if (shook == false)
            {
                StartCoroutine(isShook(1f));
            }

            door_eyes.SetActive(true);
        }

        if (resistScript.winner == true)
        {
            //do a backing away anim for enemy
            //enemyForFight.GetComponent<SpriteRenderer>().enabled = false;
            //do this in resist in action
        }

        if (resistScript.startFight == true)
        {
            speed = 0;
        }

    
    }

    IEnumerator isShook(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cameraShakeScript.ScreenShake(0.1f, 1f);
        yield return new WaitForSeconds(1f);
        shook = true;

    }

    IEnumerator enemyIn(float waitTime)
    {
        //cameraShakeScript.ScreenShake(0.1f, 1f);

        yield return new WaitForSeconds(waitTime);
        enemyForFight.SetActive(true);

        //follow player
        //Vector3 localPosition = player.transform.position - enemyForFight.transform.position;
        //localPosition = localPosition.normalized;
        //enemyForFight.transform.Translate(localPosition.x * Time.deltaTime * speed, 0, localPosition.z * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player!");
            triggered = true;
        }
    }

    IEnumerator enemyAtStart()
    {
        yield return new WaitForSeconds(0.5f);
        //start scene enemy
        player.GetComponent<playerController>().walkSpeed = 0;
        cameraShakeScript.ScreenShake(0.1f, 2f);
        //enemy does desctroy animation
        yield return new WaitForSeconds(2.8f);
        player.GetComponent<playerController>().walkSpeed = 3;
        enemyStart.SetActive(false);
    }
}
