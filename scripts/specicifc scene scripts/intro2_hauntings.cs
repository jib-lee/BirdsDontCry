using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro2_hauntings : MonoBehaviour
{
    public Interact_Look windowLook;
    public Animator windowHauntAnim;

    public Animator doorAnim;
    public AudioSource audCreak;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (windowLook.canInspect == false)
        {
            //windowHauntAnim.SetTrigger("haunt");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorAnim.SetTrigger("open");
            audCreak.Play();
        }
    }
}
