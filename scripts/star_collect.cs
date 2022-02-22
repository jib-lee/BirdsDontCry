using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_collect : MonoBehaviour
{
    //put on other star collectibles that's not the first one

    public GameObject handSprite;
    bool canInspect;

    public KeyCode inspectKey = KeyCode.UpArrow;

    void Start()
    {
        handSprite.SetActive(false);
    }

    void Update()
    {
        if (canInspect)
        {

            if (Input.GetKeyDown(inspectKey))
            {
                //play sound?
                this.gameObject.SetActive(false);
            }

        }
        else if (canInspect == false)
        {
            handSprite.SetActive(false);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            handSprite.SetActive(true);
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
