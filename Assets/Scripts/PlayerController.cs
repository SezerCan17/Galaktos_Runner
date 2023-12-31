using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runningSpeed;
    public float xSpeed;
    public float limitX;
    public GameManager GameManager;
    public Animator myanims;
    public GameObject Player;
   

    private void Awake()
    {
        myanims = GetComponent<Animator>();
        
        
        
        
    }
    void Start()
    {
        
    }


    void Update()
    {
        
        SwipeCheck();
        
    }

    void SwipeCheck()
    {
        if (GameManager.GameStarted==true)
        {

            myanims.SetTrigger("speed");
            float newX = 0;
            float touchXDelta = 0;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //Debug.Log(Input.GetTouch(0).deltaPosition.x / Screen.width);
                touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;

            }
            else if (Input.GetMouseButton(0))
            {
                touchXDelta = Input.GetAxis("Mouse X");
            }
            newX = transform.position.x + xSpeed * Time.deltaTime * touchXDelta;
            newX = Mathf.Clamp(newX, -limitX, limitX);

            
            Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);
            transform.position = newPosition;
            
        }
        

    }

  
}
