using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level5_throwPlayer : MonoBehaviour
{
    public GameObject player;
    public int force;

    public BoxCollider2D house;
    bool enableCollider = false;

    public Animator playerAnim;
    public AudioSource windowBreakSound;
    public Animator windowAnim;

    void Start()
    {
        StartCoroutine(ThrowPlayer());
        playerAnim.SetBool("gotThrown", true);
        house.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableCollider == true)
        {
            house.enabled = true;
        }
    }

    IEnumerator ThrowPlayer()
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        yield return new WaitForSeconds(0.65f);
        windowBreakSound.Play();
        windowAnim.SetTrigger("break");
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        player.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
        enableCollider = true;
    }
    

}
