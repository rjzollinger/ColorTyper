﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class SpawnBall : MonoBehaviour
{
    public GameObject ButtonL;
    public GameObject ButtonC;
    public GameObject ButtonR;

    public GameObject manager;

    public GameObject ball;
    public float speed;
    public Transform player;
    public bool ballExists;

    private Color[] ballColors;

    // Start is called before the first frame update
    void Start()
    {
        ballColors = LoadBallColors();

        speed = 10;

        ballExists = false;
    }

    Color[] LoadBallColors()
    {
        TextAsset ballDataString = Resources.Load<TextAsset>("ballcolors");
        BallData ballData = JsonUtility.FromJson<BallData>(ballDataString.ToString());
        return ballData.colors;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballExists && !manager.GetComponent<Manager>().gameLost)
        {
            GameObject newBall = Instantiate(ball, transform.position, Quaternion.identity) as GameObject;
            newBall.GetComponent<MoveBall>().player = player;
            newBall.GetComponent<MoveBall>().speed = speed;

            Color ballColor = ballColors[Random.Range(0, ballColors.Length)];
            newBall.GetComponent<Renderer>().material.color = ballColor;
            
            ButtonL.GetComponent<CheckCorrect>().currentBall = newBall;
            ButtonC.GetComponent<CheckCorrect>().currentBall = newBall;
            ButtonR.GetComponent<CheckCorrect>().currentBall = newBall;
            AssignAllButtonColors(ballColor);
            ballExists = true;
        }
    }

    void AssignAllButtonColors (Color trueColor)
    {
        int correctIndex = Random.Range(0,3);
        //Debug.Log(correctIndex);
        switch (correctIndex)
        {
            case 0:
            {
                AssignSingleButtonColor(ButtonL.GetComponent<Button>(), trueColor);
                AssignSingleButtonColor(ButtonC.GetComponent<Button>(), Random.ColorHSV());
                AssignSingleButtonColor(ButtonR.GetComponent<Button>(), Random.ColorHSV());
                break;
            }
            
            case 1:
            {
                AssignSingleButtonColor(ButtonL.GetComponent<Button>(), Random.ColorHSV());
                AssignSingleButtonColor(ButtonC.GetComponent<Button>(), trueColor);
                AssignSingleButtonColor(ButtonR.GetComponent<Button>(), Random.ColorHSV());
                break;
            }

            case 2:
            {
                AssignSingleButtonColor(ButtonL.GetComponent<Button>(), Random.ColorHSV());
                AssignSingleButtonColor(ButtonC.GetComponent<Button>(), Random.ColorHSV());
                AssignSingleButtonColor(ButtonR.GetComponent<Button>(), trueColor);
                break;
            }
        }
    }

    void AssignSingleButtonColor (Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = color;
        cb.selectedColor = color;
        button.colors = cb;
    }
}
