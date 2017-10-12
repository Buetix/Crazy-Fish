using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float moveSpeed;
    public bool isAlive;

    [SerializeField]
    private Button upButton;
    [SerializeField]
    private Button downButton;
    [SerializeField]
    private Animator anim;

    private Rigidbody2D myRigidbody;
    
    private GameController theGameController;
    private PlayerHP playerHP;

    /*AUDIO CONTROLLER*/
    public AudioSource audioSourceGetScore, audioSourceDead, audioSourceGetKey, audioSourceMove, audioSourceNextLevel;

    // Use this for initialization
    void Start () {
        isAlive = true;
        myRigidbody = GetComponent<Rigidbody2D>();
                
        theGameController = FindObjectOfType<GameController>();
        playerHP = FindObjectOfType<PlayerHP>();
	}
	
	// Update is called once per frame
	void Update () {
        upButton.onClick.AddListener(() => UpButtonClick());
        downButton.onClick.AddListener(() => DownButtonClick());
        /*if(playerHP.currentHP == 0)
        {
            if(isAlive == true)
            {
                isAlive = false;
                audioSourceDead.Play();
                anim.SetTrigger("Died");
                theGameController.gameOverMucis.Play();
            }
            
        }*/


	}

    public void UpButtonClick()
    {
        myRigidbody.velocity = new Vector3(0f, moveSpeed, 0f);
        myRigidbody.gravityScale = 0.07f;

        
        audioSourceMove.Play();
    }

    public void DownButtonClick()
    {
        myRigidbody.velocity = new Vector3(0f, -moveSpeed, 0f);
        myRigidbody.gravityScale = -0.07f;

        
        audioSourceMove.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "GetScore")
        {
            theGameController.currentScore++;
            audioSourceGetScore.Play();
        }

        if(collision.tag == "Key")
        {
            theGameController.numberKey++;
            theGameController.currentScore += 5; 
            Destroy(collision.gameObject);

            audioSourceGetKey.Play();
            if(playerHP.currentHP < playerHP.totalHP)
            {
                playerHP.currentHP += 1f;
            }
        }
        if(collision.tag == "KeyMiss")
        {
            if(playerHP.currentHP <= 1f)
            {
                if (isAlive == true)
                {
                    playerHP.currentHP -= 1f;
                    theGameController.currentScore -= 5;
                    isAlive = false;
                    audioSourceDead.Play();
                    anim.SetTrigger("Died");
                    theGameController.gameOverMucis.Play();
                    //Time.timeScale = 0;
                }
            }else if (playerHP.currentHP > 0f)
            {
                playerHP.currentHP -= 1;
                theGameController.currentScore -= 5;
            }
            

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(isAlive == true)
            {
                isAlive = false;
                playerHP.currentHP = 0;
                audioSourceDead.Play();
                anim.SetTrigger("Died");
                theGameController.gameOverMucis.Play();
                //Time.timeScale = 0;
            }
            

        }
    }
}
