using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public TMP_Text scoreDisplay;
    public TMP_Text highScoreDisplay;

    public bool gameLost;
    private int highScore;
    public int ballsCorrect;
    public int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        gameLost = false;
        ballsCorrect = 0;
        currentScore = 0;

        //load in and display high score
        if (File.Exists(Application.persistentDataPath + "/save.json"))
        {
            LoadHighScore();
        }
        else
        {
            highScore = 0;
        }

        highScoreDisplay.SetText("High Score: " + highScore);
    }

    void LoadHighScore()
    {
        string saveString = File.ReadAllText(Application.persistentDataPath + "/save.json");
        Save highScoreSave = JsonUtility.FromJson<Save>(saveString);
        highScore = highScoreSave.highScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        SetUIScore();

        if (gameLost)
        {
            EndGame();
        }
    }

    void SetUIScore()
    {
        scoreDisplay.SetText("Current Score: " + currentScore.ToString());
    }

    void EndGame()
    {
        if (currentScore > highScore)
        {
            CreateGameSave(currentScore);
        }
    }

    private void CreateGameSave(int currentScore)
    {
        Save save = new Save();
        save.highScore = currentScore;
        string saveJson = JsonUtility.ToJson(save);

        File.WriteAllText(Application.persistentDataPath + "/save.json", saveJson);
    }
}
