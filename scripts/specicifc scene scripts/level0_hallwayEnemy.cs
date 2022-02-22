using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level0_hallwayEnemy : MonoBehaviour
{
    public GameObject collectible;
    public GameObject enemy;
    public GameObject windowHole;

    public resist_InAction resistScript;

    int speed = 1;

    public bool triggered;

    public GameObject player;

    void Start()
    {
        enemy.SetActive(false);
        windowHole.SetActive(false);
        triggered = false;
    }

    void Update()
    {
        if (collectible.activeSelf == false && resistScript.startFight == false)
        {
            StartCoroutine(enemyIn());
            

        } else if (triggered && collectible.activeSelf == true)
        {
   
            StartCoroutine(enemyIn());
          
        }

        if(resistScript.winner == true)
        {
            //do a backing away anim for enemy
            //enemy.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (resistScript.startFight == true)
        {
            speed = 0;
        }

      
    }

    IEnumerator enemyIn()
    {

        
        yield return new WaitForSeconds(1f);

        //glass breaking sound && player animation? (player cant move during animation?)
        windowHole.SetActive(true);
        //enemy comes in
        enemy.SetActive(true);
        //enemy.transform.Translate(Vector2.right * Time.deltaTime * speed);

        ////follow player
        //Vector3 localPosition = player.transform.position - enemy.transform.position;
        //localPosition = localPosition.normalized;
        //enemy.transform.Translate(localPosition.x * Time.deltaTime * speed, 0, localPosition.z * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player!");
            triggered = true;
            
        }
    }
}
