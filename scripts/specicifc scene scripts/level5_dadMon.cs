using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level5_dadMon : MonoBehaviour
{
    public GameObject player;
    public float stopDistance;
    float distToplayer;

    public Animator bossEnemyAnim;

    bool moving;

    public bossEnemy_resistInAction bossScript;

    void Start()
    {
        bossScript.enabled = false;

        moving = true;
    }

    
    void Update()
    {
 
        if (moving)
        {
            FollowPlayer();
        } else if (!moving)
        {
            speed = 0;
        }

        distToplayer = this.gameObject.transform.position.x - player.gameObject.transform.position.x;
        if (distToplayer <= stopDistance)
        {
            player.GetComponent<playerController>().walkSpeed = 0;
            moving = false;
            StartCoroutine(startMorph());
        }
    }

    IEnumerator startMorph()
    {

        //boss is the flower groing on top
        bossScript.enabled = true;
        yield return new WaitForSeconds(0.75f);
        bossEnemyAnim.SetBool("morph", true);
        
        //do morph animation and wait for certain seconds

        
    }

    int speed = 1;

    void FollowPlayer()
    {
        Vector3 localPosition = player.transform.position - this.transform.position;
        localPosition = localPosition.normalized;
        this.transform.Translate(localPosition.x * Time.deltaTime * speed, 0, localPosition.z * Time.deltaTime * speed);
    }
}
