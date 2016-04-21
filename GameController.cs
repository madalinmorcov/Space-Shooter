using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardsCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;


    private int score;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText highScoreText;

    private bool gameOver;
    private bool restart;

    void Start()
    {
        gameOver = false;
        restart = false;
        scoreText.text = "Score: ";
        restartText.text = "";
        gameOverText.text = "";
        highScoreText.text = "";

        score = 0;
        updateScore();
        StartCoroutine  (SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if(Input.GetKey(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

        }

    }

    public void GameOver()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", score);
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
        gameOverText.text = "Game Over!";
        gameOver = true;
        restart = true;
        restartText.text = "Press 'R' for Restart!";
    }





    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardsCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if(gameOver)
            {
                break;
            }
        }

    }
    /*  Vectorul hazards retine tipurile de elemente inamice,asteroizii si nava inamica.
        Variabila hazardsCount este initializata din interfata si retine numarul de elemente care apar la fiecare val de inamici.
        Pentru fiecare element instantiat, ales random, se creeaza o pozitie de instantiere random de asemenea aflata intre
        doua coordonate date pe axa OX pentru a nu depasi parametriul jocului.
        Obiectul este instantiat la pozitia creata cu rotatia setata la 0, adica aliniata cu "lumea jocului"
        */










    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        updateScore();
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
        
    }
	
}
