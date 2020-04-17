using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public GameObject originalPipe;
    public float pipeSpacing = 3f;
    public float gapRange = 3f;
    public GameObject canvasGameOver;
    public Text textScore;
    public Text textBestScore;
    public int score = 0;
    public int best = 0;
    public bool gameOver = false;
    public AudioSource audioScore;
    public AudioSource audioBest;
    public AudioSource audioOver;
    public AudioSource audioBG;
    public int s = 0;
    
    void Start()
    {
        GenerateLevel();
        if (PlayerPrefs.HasKey("BEST"))
        {
            best = PlayerPrefs.GetInt("BEST");
        }
        audioBG.Play();
    }
    void Update()
    {

    }

    public void GenerateLevel()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject pipe = Instantiate(originalPipe);
            pipe.transform.position = new Vector3(i * pipeSpacing, Random.Range(-gapRange, gapRange), 0);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        canvasGameOver.SetActive(true);
        textBestScore.text = PlayerPrefs.GetInt("BEST").ToString();
        audioOver.Play();
        audioBG.Stop();
    }

    public void AddScore()
    {
        if (gameOver)
            return;
        
        // Because 4 elements compose "Super Bird", otherwise the score is multiplied by 4...
        
        
        if (s == 3) {
            score++;
            s = -1;

            audioScore.Play();
        
            if (score > best)
            {
                audioBest.Play();
                PlayerPrefs.SetInt("BEST", score);
                PlayerPrefs.Save();
            }

        }
        s++;


        textScore.text = score.ToString();
        
        
    }

    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("BEST", 0);
        PlayerPrefs.Save();
    }


}

