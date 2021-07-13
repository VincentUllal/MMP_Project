using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private AudioSource audioS;
    public static int scoreAmount;
    public int requiredScore = 3;
    private Text scoreText;
    private bool doorOpen = false;
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreAmount = 0;
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreAmount + " / " + requiredScore;
        if (scoreAmount >= requiredScore && !doorOpen){
            audioS.Play();
            Debug.Log("door is open");
            doorOpen = true;
            //SoundManager.PlaySound("ZeldaSecret");
            //open a door or something
        }
    }
}
