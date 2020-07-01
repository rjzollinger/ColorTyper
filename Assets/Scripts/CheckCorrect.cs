using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CheckCorrect : MonoBehaviour
{
    public GameObject currentBall;
    public GameObject manager;
    public GameObject spawner;

    void Start()
    {
        Button thisButton = this.GetComponent<Button>();
		thisButton.onClick.AddListener(CheckIfCorrect);
    }

    void CheckIfCorrect()
    {
        if (!manager.GetComponent<Manager>().gameLost)
        {
            Button thisButton = this.GetComponent<Button>();
            if (Color.Equals(thisButton.colors.normalColor, currentBall.GetComponent<Renderer>().material.color))
            {
                //correct button pressed
                manager.GetComponent<Manager>().currentScore += (int) Math.Pow((double) 2, (double) (manager.GetComponent<Manager>().ballsCorrect / 6));
                manager.GetComponent<Manager>().ballsCorrect += 1;
                Destroy(currentBall);
                spawner.GetComponent<SpawnBall>().speed += 1;
                //Debug.Log(spawner.GetComponent<SpawnBall>().speed);
                spawner.GetComponent<SpawnBall>().ballExists = false;
                //Debug.Log(manager.GetComponent<Manager>().ballsCorrect);
            }
        }
    }
}