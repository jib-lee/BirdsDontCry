using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class resist_InAction : MonoBehaviour
{
    float counter;
    public float delay;
    public float difficulty; //the higher the harder
    float enemyPowerLevel;
    public float maxEnemyPowerLevel;
    public Slider powerSlider;
    public GameObject resistUI;
    public bool winner;
    public bool loser;

    //public SpriteRenderer playerSp;
    //public SpriteRenderer enemySp;

    public GameObject player;
    public LayerMask playerLayer;
    public int distToPlayer;

    private Rigidbody2D rb;

    public GameObject enemy;

    public GameObject loseCanvas;
    public string sceneName;
    int direction;

    public Animator playerAnim;

    public GameObject enemyBodyGroup;
    float someScale;
    public Animator enemyAnim;

    public GameObject border;
    public Animator borderAnim;

    AudioSource audioSource;
    public GameObject loseSoundObj;

    void Start()
    {
        enemyPowerLevel = maxEnemyPowerLevel / 2;
        resistUI.SetActive(false);
        powerSlider.maxValue = maxEnemyPowerLevel;
        powerSlider.value = enemyPowerLevel;
        winner = false;
        loser = false;

        //rb = enemy.GetComponent<Rigidbody2D>();

        powerSlider.interactable = false;

        loseCanvas.SetActive(false);

        someScale = enemyBodyGroup.transform.localScale.x;
        audioSource = GetComponent<AudioSource>();
        loseSoundObj.SetActive(false);

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
            enemyBodyGroup.transform.localScale = new Vector2(someScale, enemyBodyGroup.transform.localScale.y);

        }
        else if (direction == -1)
        {
            enemyBodyGroup.transform.localScale = new Vector2(-someScale, enemyBodyGroup.transform.localScale.y);
        }

        if (startFight == true && !winner && !loser)
        {

            StartCoroutine(FadeAudioSource.StartFade(audioSource, 1f, 1f));
            border.SetActive(true);
            resistUI.SetActive(true);
            player.GetComponent<playerController>().walkSpeed = 0;
            playerAnim.SetBool("startCrouch", true);
            enemyAnim.SetBool("attack", true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                counter = 0;
            }

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
            else if (enemyPowerLevel <= 0)
            {
                winner = true;
            }
            powerSlider.value = enemyPowerLevel;
        }

        if (loser)
        {
            loseSoundObj.SetActive(true);
            StartCoroutine(Loser());
        }

        if (winner)
        {
            //getting free animation
            borderAnim.SetBool("exit", true);
            resistUI.SetActive(false);
            player.GetComponent<playerController>().walkSpeed = 3f;
            knockedback = true;
            playerAnim.SetBool("startCrouch", false);
            enemyAnim.SetBool("attack", false);

            StartCoroutine(FadeAudioSource.StartFade(audioSource, 3f, 0f));

        }

        if (loseCanvas.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SceneManager.LoadScene(sceneName);
            }
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
            loseSoundObj.SetActive(true);
            enemyAnim.SetBool("attack", true); //chang to claw swing
            player.GetComponent<playerController>().walkSpeed = 0;
            speed = 0;
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
        
        borderAnim.SetBool("playerDie", true);
        borderAnim.SetBool("exit", true);
        enemyAnim.SetBool("lose", true); //chang to claw swing
        player.GetComponent<playerController>().walkSpeed = 0;
        playerAnim.SetBool("dead", true);
        resistUI.SetActive(false);
        yield return new WaitForSeconds(1.5f); //wait to die
                                               //die anim and restart scene

        //canvas for restart
        loseCanvas.SetActive(true);

    }

    public bool knockedback = false;
    int repeatCounter = 0;

    IEnumerator KnockBack()
    {
        //knockback befor followinf again
        //play knocked back anim
        Debug.Log("knocked back!");
        enemyAnim.SetBool("knocked", true);
        speed = 0;
        yield return new WaitForSeconds(3f);
        speed = 2;
        repeatCounter += 1;
        enemyAnim.SetBool("knocked", false);

    }

    int speed = 2;


    IEnumerator FollowPlayer()
    {
        //follow player
        enemyAnim.SetBool("run", true);
        Vector3 localPosition = player.transform.position - enemy.transform.position;
        localPosition = localPosition.normalized;
        enemy.transform.Translate(localPosition.x * Time.deltaTime * speed, 0, localPosition.z * Time.deltaTime * speed);

        yield return null;
    }


    void OnTriggerStay2D(Collider2D collision)
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
}
