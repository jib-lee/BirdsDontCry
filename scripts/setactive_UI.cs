using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setactive_UI : MonoBehaviour
{

    //set active UI and flowchart

    public GameObject look_sprite;
    public bool canInspect;

    public KeyCode inspectKey = KeyCode.UpArrow;
    public GameObject flowCh;

    int count = 0;

    void Start()
    {
        look_sprite.SetActive(false);
        canInspect = false;
        flowCh.SetActive(false);
    }

    void Update()
    {
        if (canInspect)
        {
            if (Input.GetKeyDown(inspectKey))
            {
                flowCh.SetActive(true);
                count += 1;
            }

        }
        else if (canInspect == false)
        {

            look_sprite.SetActive(false);

        }

        if(count >= 1)
        {
            look_sprite.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            look_sprite.SetActive(true);
            canInspect = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInspect = false;
        }
    }
}
