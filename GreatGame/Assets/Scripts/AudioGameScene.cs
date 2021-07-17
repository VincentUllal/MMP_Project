using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Audio
{
    public class AudioGameScene : MonoBehaviour
    {
        private static readonly string bgPref = "BackgroundPref";
        private static readonly string sfxPref = "SoundEffectsPref";
        private float bgFloat, sfxFloat;
        public AudioSource bgAudio;
        public AudioSource[] sfxAudio;

        private void Awake()
        {
            ContinueSettings();
        }

        private void ContinueSettings()
        {
            bgFloat = PlayerPrefs.GetFloat(bgPref);
            sfxFloat = PlayerPrefs.GetFloat(sfxPref);

            bgAudio.volume = bgFloat;

            for (int i = 0; i < sfxAudio.Length; i++)
            {
                sfxAudio[i].volume = sfxFloat;
            }
        }
    }
}
