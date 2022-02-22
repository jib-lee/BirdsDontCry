using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossEnemy_resistInAction : MonoBehaviour
{
    //scripted lose for the player
    float counter;
    public float delay;
    public float difficulty; //the higher the harder
    float enemyPowerLevel;
    public float maxEnemyPowerLevel;
    public Slider powerSlider;
    public GameObject resistUI;
    public bool winner;
    public bool loser;

    public GameObject player;
    public LayerMask playerLayer;
    public int distToPlayer;

    private Rigidbody2D rb;

    public GameObject enemy;

    public string sceneName;

    private SpriteRenderer sr;
    int direction;

    public Animator playerAnim;

    public GameObject enemyBodyGroup;
    float someScale;
    public Animator bossAnim;

    public GameObject border;
    public Animator borderAnim;

    AudioSource audioSource;
    public GameObject loseSoundObj;

    public GameObject star_group;
    public AudioSource bgm;

    void Start()
    {
        enemyPowerLevel = maxEnemyPowerLevel / 2;
        resistUI.SetActive(false);
        powerSlider.maxValue = maxEnemyPowerLevel;
        powerSlider.value = enemyPowerLevel;
        winner = false;
        loser = false;

        someScale = enemyBodyGroup.transform.localScale.x;

        powerSlider.interactable = false;

        sr = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
        loseSoundObj.SetActive(false);

        star_group.SetActive(false);

    }

    void Update()
    {
        if (startFight == false)
        {
            StartCoroutine(FollowPlayer());
        }

        bool playerIsClose = Physics2D.OverlapCircle(transform.position, distToPlayer, playerLayer);

        if (playerIsClose)
        {
            startFight = true;
        }

        //flip
        //make sprite flip code!
        if (player.transform.position.x - transform.position.x > 0)
        {
            direction = 1;
        }
        else if (player.transform.position.x - transform.position.x < 0)
        {
            direction = -1;
        }

        if (direction == 1)
        {
            enemyBodyGroup.transform.localScale = new Vector2(-someScale, enemyBodyGroup.transform.localScale.y);

        }
        else if (direction == -1)
        {
            enemyBodyGroup.transform.localScale = new Vector2(someScale, enemyBodyGroup.transform.localScale.y);
        }

        if (startFight == true && !winner && !loser)
        {
            resistUI.SetActive(true);
            player.GetComponent<playerController>().walkSpeed = 0;
            playerAnim.SetBool("startCrouch", true);

            border.SetActive(true);
            StartCoroutine(FadeAudioSource.StartFade(audioSource, 1f, 1f));

            counter += Time.deltaTime;
            if (counter <= delay)
            {
                enemyPowerLevel -= Time.deltaTime / difficulty;
            }
            else
            {
                enemyPowerLevel += Time.deltaTime;
            }

            if (enemyPowerLevel >= maxEnemyPowerLevel)
            {
                loser = true;
            }
       
            powerSlider.value = enemyPowerLevel;
        }

        if (loser)
        {
            StartCoroutine(Loser());
            loseSoundObj.SetActive(true);
        }

        if (winner)
        {
            //getting free animation
            resistUI.SetActive(false);
            player.GetComponent<playerController>().walkSpeed = 3f;
            knockedback = true;

        }

        if (knockedback && repeatCounter <= 0)
        {
            StartCoroutine(KnockBack());
        }
        if (winner && knockedback && repeatCounter >= 1)
        {
            StartCoroutine(FollowPlayer());
        }

        if (winner && dieOnsecondHit)
        {
            player.GetComponent<playerController>().walkSpeed = 0;
        }

    }

    public bool startFight = false;

    public bool dieOnsecondHit = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (winner && repeatCounter >= 1)
            {
                //dead if won but caught again
                dieOnsecondHit = true;
                StartCoroutine(Loser());
            }
        }
    }



    IEnumerator Loser()
    {
        //this is a scripyed lose, player will activate light to help
        
        yield return new WaitForSeconds(2f);
        borderAnim.SetBool("exit", true);
        resistUI.SetActive(false);
        //activate lights & do their animations
        star_group.SetActive(true);
        playerAnim.SetBool("lookAtLights", true);

        bossAnim.SetBool("die", true);
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 2f, 0f));
        yield return new WaitForSeconds(1f); // the duration of this is how long the light anima take


        //fade to white then change scene
       
        StartCoroutine(FadeAudioSource.StartFade(bgm, 3f, 0f));
        StartCoroutine(FadeTo(1f, 1f));
        yield return new WaitForSeconds(4f);
   //to next scene to see dad
        SceneManager.LoadScene(sceneName);

    }
    public Image white_image;

    public IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = white_image.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            white_image.color = newColor;
            yield return null;
        }
    }

    public bool knockedback = false;
    int repeatCounter = 0;

    IEnumerator KnockBack()
    {
        //knockback befor followinf again
        //play knocked back anim
        Debug.Log("knocked back!");
        speed = 0;
        yield return new WaitForSeconds(5f);
        speed = 1;
        repeatCounter += 1;


    }

    public int speed;


    IEnumerator FollowPlayer()
    {
        //follow player
        Vector3 localPosition = player.transform.position - enemy.transform.position;
        localPosition = localPosition.normalized;
        enemy.transform.Translate(localPosition.x * Time.deltaTime * speed, 0, localPosition.z * Time.deltaTime * speed);

        yield return null;
    }

}
