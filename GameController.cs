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
    public float secondSpawnEneny; //bao nhieu giay se sinh ra enemy
    public float enemyMoveSpeed; //toc do cua enemy

    [SerializeField]
    private GameObject[] enemy;
    [SerializeField]
    private GameObject enemyPosition;

    /*KEY CONTROLLER*/
    public float secondSpawnKey; //bao nhieu giay se sinh ra key
    public float keyMoveSpeed; //toc to cua key

 
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
    public GameObject nextLevelParticleSystem;//khi next level se hien hieu ung cua ParticleSystem
    [SerializeField]
    private GameObject nextLevelShow;
    //private bool isNextLevel;
    
    /*TEMP VARIABLE - USED FOR WHEN UPGRATE LEVEL*/
    private int tempNumberKeyPassLevel;// bien nay muc dich la luu cai numberKeyPassLevel de su dung cho muc dich khi tang level se tang so luong key passlevel
    private float tempSecondSpawnEnemy;
    private float tempEnemyMoveSpeed;

    /*MUSIC*/
    public AudioSource gameOverMucis, gamePlayMusic;

    void Start()
    {
        //Thiet lap he thong PlayerPrefs: endScore, bestScore, numberKey, level
        PlayerPrefs.SetInt("CURRENT_SCORE", 0);
        currentScore = PlayerPrefs.GetInt("CURRENT_SCORE");

        //PlayerPrefs.SetInt("BEST_SCORE", 0);
        //bestScore = PlayerPrefs.GetInt("BEST_SCORE");

        PlayerPrefs.SetInt("KEY", 0);
        numberKey = PlayerPrefs.GetInt("KEY");

        PlayerPrefs.SetInt("LEVEL", 1);
        level = PlayerPrefs.GetInt("LEVEL");

        //Tu sinh ra key va enemy
        StartCoroutine(EnemySpawner());
        StartCoroutine(KeySpawner(key));

        //Dua score, number key, level ra ngoai textbox hien thi len man hinh
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

    //Ham dung de tao ra enemy 
    IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(secondSpawnEneny);
        Vector3 tempEnemy = enemyPosition.transform.position;//lay vi tri cua enemyPosition de tu do sinh ra enemy
        tempEnemy.y = UnityEngine.Random.Range(-3.5f, 3.5f);

        Instantiate(this.enemy[RandomSpawnEnemy(this.level)], tempEnemy, Quaternion.identity);
        StartCoroutine(EnemySpawner());

    }

    //Ham dung de tao ra key
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

    //Khi gom du so key duoc set trong numberKeyPassLevel se tu dong next level
    private void NextLevel(int level)
    {
        if(numberKey == numberKeyPassLevel)
        {
            level++;
            //this.isNextLevel = true;
            //khi tang level se xuat hien hieu ung
            Instantiate(nextLevelParticleSystem, player.transform.position, player.transform.rotation);
            
            //khi tang level se phat am thanh sound next level
            player.audioSourceNextLevel.Play();
            
            //Khi tang 1 level se tang them 1 don vi cua numberKeyPassLevel 
            numberKeyPassLevel = tempNumberKeyPassLevel * level;

            PlayerPrefs.SetInt("LEVEL", level);
            this.level = PlayerPrefs.GetInt("LEVEL");

            //Khi tang level se giam thoi gian spawnenemy, nghia la tan suat xuat hien tang len
            secondSpawnEneny = tempSecondSpawnEnemy - level * 0.2f;

            //Khi tang level se tang toc do cua enemy la + 0.2f cho moi level
            enemyMoveSpeed = tempEnemyMoveSpeed + level * 0.2f;
            //nextLevelShow.SetActive(true);

        }
    }

    private int RandomSpawnEnemy(int level)
    {
        switch (level)
        {
            case 1:
                return 0; //sinh ra shark enemy 1
                //break;
            case 2:
                return 1; // sinh ra shark enemy 2
                //break;
            case 3:
                return 2; // sinh ra shark enemy 3
               // break;
            case 4:
                return 3; // sinh ra octopus
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
