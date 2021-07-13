using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class SoundManager : MonoBehaviour
    {
        public  AudioClip change_TimerBlock;
        private AudioSource audioSrc;
        void Start()
        {
            change_TimerBlock = Resources.Load<AudioClip>("Change_TimerBlock");
            audioSrc = GetComponent<AudioSource>();
        }

        public void PlaySound(string clip)
        {
            switch (clip)
            {
                case "Change_TimerBlock":
                    audioSrc.PlayOneShot(change_TimerBlock);
                    break;
            }
        }
    }
}