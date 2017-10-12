using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int currentScore;
    //public int bestScore;
    public int numberKey;
    public int numberKeyPassLevel;
    public int level;

    /*public float currentHP;
    public float totalHP;
    [SerializeField]
    private Image currentHPImage;*/

    [SerializeField]
    private Text scoreText, keyText, levelText;

    /*ENEMY CONTROLLER*/
    public float secondSpawnEneny; //how long it takes the enemy to spawn
    public float enemyMoveSpeed; //how fast is the enemy

    [SerializeField]
    private GameObject[] enemy;
    [SerializeField]
    private GameObject enemyPosition;

    /*KEY CONTROLLER*/
    public float secondSpawnKey; //how long it takes for the key to spawn
    public float keyMoveSpeed; 

 
    [SerializeField]
    private GameObject key;
    [SerializeField]
    private GameObject keyPosition;

    /*GAME OVER*/
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text endScoreText, bestScoreText;

    /*PLAYER CONTROLLER*/
    private Player player;

    /*NEXT LEVEL*/
    public GameObject nextLevelParticleSystem;//levelup will play an animation
    [SerializeField]
    private GameObject nextLevelShow;
    //private bool isNextLevel;
    
    /*TEMP VARIABLE - USED FOR WHEN UPGRATE LEVEL*/
    private int tempNumberKeyPassLevel;
    private float tempSecondSpawnEnemy;
    private float tempEnemyMoveSpeed;

    /*MUSIC*/
    public AudioSource gameOverMucis, gamePlayMusic;

    void Start()
    {
        
        PlayerPrefs.SetInt("CURRENT_SCORE", 0);
        currentScore = PlayerPrefs.GetInt("CURRENT_SCORE");

        //PlayerPrefs.SetInt("BEST_SCORE", 0);
        //bestScore = PlayerPrefs.GetInt("BEST_SCORE");

        PlayerPrefs.SetInt("KEY", 0);
        numberKey = PlayerPrefs.GetInt("KEY");

        PlayerPrefs.SetInt("LEVEL", 1);
        level = PlayerPrefs.GetInt("LEVEL");

        //Making the kay and enemy
        StartCoroutine(EnemySpawner());
        StartCoroutine(KeySpawner(key));

        //Places the score, number of keys and level to end screen
        scoreText.text = "Score: " + currentScore.ToString();
        keyText.text = numberKey.ToString();
        levelText.text = "Level: " + level.ToString();

        //Khai bao cac bien temp
        tempNumberKeyPassLevel = numberKeyPassLevel;
        tempEnemyMoveSpeed = enemyMoveSpeed;
        tempSecondSpawnEnemy = secondSpawnEneny;

        //Xu ly HP
        //currentHPImage.transform.localScale = new Vector3(currentHP / totalHP, 1f, 1f);

        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + currentScore.ToString();
        keyText.text = numberKey.ToString();
        NextLevel(level);
        levelText.text = "Level: " + level.ToString();

        if (player.isAlive == false)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            CompareScore(currentScore);
            endScoreText.text = "End score: " + PlayerPrefs.GetInt("CURRENT_SCORE").ToString();
            bestScoreText.text = "Best score: " + PlayerPrefs.GetInt("BEST_SCORE").ToString();

            gamePlayMusic.Stop();
            
        }
        //StartCoroutine(EnemySpawner(enemy[1]));
    }

    //The function to create the enemy
    IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(secondSpawnEneny);
        Vector3 tempEnemy = enemyPosition.transform.position;//sets the random position of the enemy
        tempEnemy.y = UnityEngine.Random.Range(-3.5f, 3.5f);

        Instantiate(this.enemy[RandomSpawnEnemy(this.level)], tempEnemy, Quaternion.identity);
        StartCoroutine(EnemySpawner());

    }

    //The function to create keys
    IEnumerator KeySpawner(GameObject key)
    {
        
        yield return new WaitForSeconds(secondSpawnKey);
        Vector3 tempKey = keyPosition.transform.position;
        tempKey.y = UnityEngine.Random.Range(-4.5f, 4.5f);

        Instantiate(key, tempKey, Quaternion.identity);
        StartCoroutine(KeySpawner(key));

    }

    private void CompareScore(int currentScore)
    {
        PlayerPrefs.SetInt("CURRENT_SCORE", currentScore);
        
        if(currentScore > PlayerPrefs.GetInt("BEST_SCORE"))
        {
            PlayerPrefs.SetInt("BEST_SCORE", currentScore);
        }
    }

    //When the number of keys is the same as the numberKeyPassLevel you level up
    private void NextLevel(int level)
    {
        if(numberKey == numberKeyPassLevel)
        {
            level++;
            //this.isNextLevel = true;
            Instantiate(nextLevelParticleSystem, player.transform.position, player.transform.rotation);
            
            //When a levelup is reached this sound is played
            player.audioSourceNextLevel.Play();
            
            //When you gain a level the number of keys to next level is calculated
            numberKeyPassLevel = tempNumberKeyPassLevel * level;

            PlayerPrefs.SetInt("LEVEL", level);
            this.level = PlayerPrefs.GetInt("LEVEL");

            
            secondSpawnEneny = tempSecondSpawnEnemy - level * 0.2f;

            
            enemyMoveSpeed = tempEnemyMoveSpeed + level * 0.2f;
            //nextLevelShow.SetActive(true);

        }
    }

    private int RandomSpawnEnemy(int level)
    {
        switch (level)
        {
            case 1:
                return 0; 
                //break;
            case 2:
                return 1; 
                //break;
            case 3:
                return 2; 
               // break;
            case 4:
                return 3; 
            case 5:
                return 4;
            case 6:
                return 5;
            case 7:
                return 6;
            case 8:
                return 7;
            case 9:
                return 8;
            default:
                return Random.Range(0, 9);
              //  break;

        }
    }


    public void RestartGameButton()
    {
        player.isAlive = true;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("GamePlay");
    }


    public void MenuGameButton()
    {
        player.isAlive = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}
