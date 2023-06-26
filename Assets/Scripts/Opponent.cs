using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    public NavMeshAgent OpponentAgent;
    public GameObject Target;
    public GameManager GameManager;
    Vector3 OpponentStartPos;
    Animator myanims;
    public GameObject speedBoosterIcon;
    

    private void Awake()
    {
        myanims = GetComponent<Animator>();

    }
    void Start()
    {
        OpponentAgent = GetComponent<NavMeshAgent>();
        OpponentStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
    }

    
    void Update()
    {
        if(GameManager.GameStarted==true)
        {
            myanims.SetTrigger("speed");
            OpponentAgent.SetDestination(Target.transform.position);
            
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.position = OpponentStartPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boost"))
        {
            OpponentAgent.speed = OpponentAgent.speed + 3f;
            speedBoosterIcon.SetActive(true);
            SlowAfterWhileCoroutine();
        }
        else if(other.CompareTag("Finish"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
            myanims.SetBool("finished",true);
        }
    }

    private IEnumerator SlowAfterWhileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        OpponentAgent.speed = OpponentAgent.speed - 3f;
        speedBoosterIcon.SetActive(false);
    }
}
