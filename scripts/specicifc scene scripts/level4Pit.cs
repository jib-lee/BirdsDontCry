using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level4Pit : MonoBehaviour
{
    //create the pit fall to connect to level 5

    public BoxCollider2D floor_collider;
    public GameObject floorSprite; //this can be replaced a bool to trigger animation, so that on on trigger enter --> start anim to create gap, instead of setting active stuff
    public GameObject player;
    public Shake cameraShakeScript;

    void Start()
    {
        //floorSprite.SetActive(false);
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(createPit());
        }
    }

    IEnumerator createPit()
    {
        yield return new WaitForSeconds(3f);
        //cameraShakeScript.ScreenShake(0.1f, 1f);
        //floorSprite.SetActive(true);
        floor_collider.enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
    }
}
