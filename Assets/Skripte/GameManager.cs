using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject monkeyPrefab;
    public GameObject gorillaPrefab;
    public GameObject bananaPeel;
    public GameObject ak47;

    public Transform player;

    GameObject monkey, gorilla;

    public UIManager uimanager;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bananaPeelText;
    //public TextMeshProUGUI livesText;

    public RawImage[] livesImg;

    private List<GameObject> ListbananaPeels;

    public static GameState gameState;

    private int score, lives;
    int bananaPeelAmmo;
    private float timerMonkey, timerGorilla;
    private bool spawnGorilla;

    private AudioSource enviromentSounds;
    private AudioSource ak47Reload;

    public enum GameState
    {
        start,
        running,
        gameOver,
        pause
    }
    private void Start()
    {
        ListbananaPeels = new List<GameObject>();
        enviromentSounds = GetComponent<AudioSource>();
        ak47Reload = ak47.GetComponents<AudioSource>()[0];
        ResetGame();
    }
    private void Update()
    {
        if (gameState == GameState.running)
        {
            RunningGame();
        }
        else if(gameState != GameState.pause)
        {
            foreach (GameObject go in ListbananaPeels)
            {
                Destroy(go);
            }
            ListbananaPeels.Clear();
        }


    }
    public void StartGame()
    {
        score = 0;
        lives = 3;
        bananaPeelAmmo = 3;
        timerMonkey = 4f;
        timerGorilla = 10f;
        spawnGorilla = false;

        SetActiveLivesImgs();
        scoreText.text = score.ToString();

        gameState = GameState.running;
        enviromentSounds.Play();
        uimanager.StartGame();
    }
    private void SetActiveLivesImgs()
    {
        foreach (RawImage life in livesImg)
        {
            life.gameObject.SetActive(true);
        }
    }
    public void RunningGame()
    {
        SpawnMonkeys();
        DropBananaPeel();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();   
        }
    }
    private void SpawnMonkeys()
    {
        timerMonkey -= Time.deltaTime;

        if (timerMonkey <= 0)
        {
            timerMonkey = 4f;
            monkey = Instantiate(monkeyPrefab);
            monkey.SetActive(true);
        }

        if (score == 15)
            spawnGorilla = true;

        if (spawnGorilla)
        {
            timerGorilla -= Time.deltaTime;
            if (timerGorilla <= 0)
            {
                timerGorilla = 10f;
                gorilla = Instantiate(gorillaPrefab);
                gorilla.SetActive(true);
            }
        }
    }
    private void DropBananaPeel()
    {
        if (Input.GetKeyDown(KeyCode.G) && bananaPeelAmmo > 0)
        {

            GameObject bananaPeelInst = Instantiate(bananaPeel);
            ListbananaPeels.Add(bananaPeelInst);
            bananaPeelInst.SetActive(true);
            CalculateBananaPeelAmmo();
        }
    }  
    public void LostLife(int damage)
    {
        if (lives > 0)
            for (int i = 0; i < livesImg.Length; i++)
            {
                if (lives - damage <= i && livesImg[i].IsActive())
                    livesImg[i].gameObject.SetActive(false);
            }
        lives-=damage;
        if (lives <= 0)
            GameOver();
    }
    public void KilledEnemy()
    {
        score++;
        scoreText.text = score.ToString();
    }
    public void CalculateBananaPeelAmmo()
    {
        bananaPeelAmmo--;
        SetBananaPeelText();
    }
    private void SetBananaPeelText()
    {
        bananaPeelText.text = bananaPeelAmmo + " X";
    }
    public void RestoreLife()
    {
        if (lives < 3)
            lives++;
    }
    public int GetLives()
    {
        return lives;
    }
    public RawImage[] GetImgsLives()
    {
        return livesImg;
    }
    public int GetBananaPeels()
    {
        return bananaPeelAmmo;
    }
    public void BananaPeelAmmoRestore()
    {
        bananaPeelAmmo++;
        SetBananaPeelText();
    }
    //public void DestroyBananaPeelInList(GameObject go)
    //{
    //    ListbananaPeels.Remove(go);
    //}
    public void PauseGame()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
        ak47Reload.Stop();
        enviromentSounds.Stop();
        gameState = GameState.pause;
        uimanager.Pause();
    }
    public void ContinueGame()
    {
        gameState = GameState.running;
        enviromentSounds.Play();
        uimanager.StartGame();
    }
    public void ResetGame()
    {
        gameState = GameState.start;
        uimanager.Reset();
    }
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameState = GameState.gameOver;
        enviromentSounds.Stop();
        uimanager.GameOver(score);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
