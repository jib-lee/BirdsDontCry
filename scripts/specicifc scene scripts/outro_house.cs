using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outro_house : MonoBehaviour
{

    public Animator wallAnim;
    public GameObject door;

    void Start()
    {
        door.SetActive(false);
    }

    
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wallAnim.SetBool("fade", true);
            door.SetActive(true);
        }
    }
}
