using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
     int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }   
     else
        {
            
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void AddToScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1) {
            TakeLive();
        }
        else
        {
            ResetGameSession();
        }
    }
    void TakeLive()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
     void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
