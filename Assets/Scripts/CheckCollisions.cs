using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckCollisions : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI CoinText;
    public Animator myanims;
    public GameObject player;
    public CamFollowPlayer CFP;

    // Added new codes
    public PlayerController playerController;
    Vector3 PlayerStartPos;
    public GameObject speedBoosterIcon;
    public GameObject speedBumper;
    private InGameRanking ig;
    [SerializeField] private AudioSource collectCoin;
    [SerializeField] private AudioSource Fail;
    [SerializeField] private AudioSource boost;
    [SerializeField] private AudioSource bumper;
    [SerializeField] private AudioSource finished;
    [SerializeField] private AudioSource started;
    //public Animator myanims;


    private void Start()
    {
        myanims = GetComponent<Animator>();
        PlayerStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        speedBoosterIcon.SetActive(false);
        ig = FindObjectOfType<InGameRanking>();
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            
            AddCoin();
            collectCoin.Play();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Finish"))
        {
            PlayerFinished();
            if (ig.namesTxt[4].text == "You")
            {
                Debug.Log("Congrats!..");
            }
            else
            {
                Debug.Log("You Lose!..");
            }

        }
        else if (other.CompareTag("Boost"))
        {
            playerController.runningSpeed = playerController.runningSpeed + 3f;
            boost.Play();
            bumper.Stop();
            speedBoosterIcon.SetActive(true);
            speedBumper.SetActive(false);
            StartCoroutine(SlowAfterAWhileCoroutine());
        }
        else if(other.CompareTag("Bumper"))
        {
            playerController.runningSpeed = playerController.runningSpeed - 3f;
            boost.Pause();
            bumper.Play();
            speedBoosterIcon.SetActive(false);
            speedBumper.SetActive(true);
            StartCoroutine(SlowAfterAWhileCoroutine2());
        }
    }

    void PlayerFinished()
    {
        player.transform.rotation= Quaternion.Euler(0,180,0);
        started.Stop();
        finished.Play();
        myanims.SetBool("finished", true);
        playerController.runningSpeed = 0f;
        playerController.xSpeed = 0f;
        CFP.dist.y = 2.7f;
        CFP.dist.z = -3f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Fail.Play();
            transform.position = PlayerStartPos;
        }
    }

    public void AddCoin()
    {
        score++;
        CoinText.text = "Score: " + score.ToString();
    }

    private IEnumerator SlowAfterAWhileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        playerController.runningSpeed = playerController.runningSpeed - 3f;
        speedBoosterIcon.SetActive(false);
    }
    private IEnumerator SlowAfterAWhileCoroutine2()
    {
        yield return new WaitForSeconds(2.0f);
        playerController.runningSpeed = playerController.runningSpeed + 3f;
        speedBumper.SetActive(false);
    }

}
