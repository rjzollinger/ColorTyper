using System.Collections;
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

    public Color[] colors = new Color[5];

    // Start is called before the first frame update
    void Start()
    {
        colors[0] = Color.red;
        colors[1] = Color.blue;
        colors[2] = Color.green;
        colors[3] = Color.yellow;
        colors[4] = Color.magenta;

        speed = 10;

        ballExists = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballExists && !manager.GetComponent<Manager>().gameLost)
        {
            //Debug.Log("spawning");
            //TextAsset jsonTextFile = Resources.Load("ball") as TextAsset;
            //Ball ball = JsonUtility.FromJson<Ball>(jsonTextFile.text);
            //Debug.Log(ball.gameObject);
            //GameObject newBall = Instantiate(ball.GameObject, transform.position, Quaternion.identity) as GameObject;
            //newBall.AddComponent<MoveBall>();

            GameObject newBall = Instantiate(ball, transform.position, Quaternion.identity) as GameObject;
            newBall.GetComponent<MoveBall>().player = player;
            newBall.GetComponent<MoveBall>().speed = speed;
            int colorIndex = Random.Range(0, colors.Length);
            newBall.GetComponent<Renderer>().material.color = colors[colorIndex];
            ButtonL.GetComponent<CheckCorrect>().currentBall = newBall;
            ButtonC.GetComponent<CheckCorrect>().currentBall = newBall;
            ButtonR.GetComponent<CheckCorrect>().currentBall = newBall;
            AssignAllButtonColors(colors[colorIndex]);
            ballExists = true;

            string ballJson = EditorJsonUtility.ToJson(newBall);

            File.WriteAllText(Application.persistentDataPath + "/ball.json", ballJson);
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
