using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class ControllerTimerBlock : MonoBehaviour
    {
        public bool initialVisibilityOfBlockA = true;
        public bool initialVisibilityOfBlockB = false;

        public float timerInterval = 1.0f;
        private float timer = 0;

        [SerializeField] GameObject TilemapBlockA;
        [SerializeField] GameObject TilemapBlockB;
        [SerializeField] AudioSource SoundSource;

        void Start()
        {
            if (initialVisibilityOfBlockA != TilemapBlockA.activeSelf)
                TilemapBlockA.SetActive(!TilemapBlockA.activeSelf);

            if (initialVisibilityOfBlockB != TilemapBlockB.activeSelf)
                TilemapBlockB.SetActive(!TilemapBlockB.activeSelf);
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer > timerInterval)
            {
                TilemapBlockA.SetActive(!TilemapBlockA.activeSelf);
                TilemapBlockB.SetActive(!TilemapBlockB.activeSelf);

                timer = 0;

                if (SoundSource.isPlaying)
                    Debug.Log("TimeBlock Interval is shorter than the link Audioclip");

                SoundSource.Play();     
                
            }
        }
    }
}