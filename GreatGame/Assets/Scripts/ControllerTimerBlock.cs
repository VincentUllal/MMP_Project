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
        private bool blockA_playing = true;

        [SerializeField] GameObject TilemapBlockA;
        [SerializeField] GameObject TilemapBlockB;
        [SerializeField] AudioSource SoundSource_A;
        [SerializeField] AudioSource SoundSource_B;

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
                if(blockA_playing){
                    TilemapBlockA.SetActive(!TilemapBlockA.activeSelf);
                    TilemapBlockB.SetActive(!TilemapBlockB.activeSelf);

                    timer = 0;

                    if (SoundSource_A.isPlaying)
                        Debug.Log("TimeBlock Interval is shorter than the link Audioclip");

                    SoundSource_A.Play();
                    blockA_playing = !blockA_playing;
                    //Debug.Log("SOUND 1");
                } else {
                    TilemapBlockA.SetActive(!TilemapBlockA.activeSelf);
                    TilemapBlockB.SetActive(!TilemapBlockB.activeSelf);

                    timer = 0;

                    if (SoundSource_B.isPlaying)
                        Debug.Log("TimeBlock Interval is shorter than the link Audioclip");

                    SoundSource_B.Play();
                    blockA_playing = !blockA_playing;
                    //Debug.Log("sound 2");
                }
                   
                
            }
        }
    }
}