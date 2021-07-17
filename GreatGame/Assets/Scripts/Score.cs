using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MMP.Score
{
    public class Score : MonoBehaviour
    {
        private AudioSource audioS;
        public static int scoreAmount;
        public int requiredScore;
        private Text scoreText;
        private bool doorOpen = false;

        [SerializeField] private GameObject portal, note;

        void Start()
        {
            requiredScore = GameObject.FindGameObjectsWithTag("key").Length;
            scoreText = GetComponent<Text>();
            scoreAmount = 0;
            audioS = GetComponent<AudioSource>();
        }

        void Update()
        {
            scoreText.text = scoreAmount + " / " + requiredScore;
            if (scoreAmount >= requiredScore && !doorOpen)
            {
                audioS.Play();
                doorOpen = true;

                portal.SetActive(true);
                note.SetActive(false);
            }
        }
    }
}