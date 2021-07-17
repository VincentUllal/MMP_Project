using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MMP.Audio
{
    public class ControllerVolume : MonoBehaviour
    {
        private static readonly string firstPlay = "FirstPlay";
        private static readonly string bgPref = "BackgroundPref";
        private static readonly string sfxPref = "SoundEffectsPref";
        private int firstInt;
        public Slider bgSlider, sfxSlider;
        private float bgFloat, sfxFloat;
        public AudioSource bgAudio;
        public AudioSource[] sfxAudio;

        private void Start()
        {
            firstInt = PlayerPrefs.GetInt(firstPlay);

            if(firstInt == 0)
            {
                bgFloat = 0.125f;
                sfxFloat = 0.75f;
                bgSlider.value = bgFloat;
                sfxSlider.value = sfxFloat;
                PlayerPrefs.SetFloat(bgPref, bgFloat);
                PlayerPrefs.SetFloat(sfxPref, sfxFloat);
                PlayerPrefs.SetInt(firstPlay, -1);
            }
            else
            {
                bgFloat = PlayerPrefs.GetFloat(bgPref);
                bgSlider.value = bgFloat;
                sfxFloat = PlayerPrefs.GetFloat(sfxPref);
                sfxSlider.value = sfxFloat;
            }
        }

        public void SaveSoundSettings()
        {
            PlayerPrefs.SetFloat(bgPref, bgSlider.value);
            PlayerPrefs.SetFloat(sfxPref, sfxSlider.value);
        }

        private void OnApplicationFocus(bool focus)
        {
            if(!focus)
            {
                SaveSoundSettings();
            }
        }

        public void UpdateSound()
        {
            bgAudio.volume = bgSlider.value;

            for(int i = 0; i < sfxAudio.Length; i++)
            {
                sfxAudio[i].volume = sfxSlider.value;
            }
        }

        public void TestSfxSound()
        {
            if (sfxAudio.Length >= 1)
            {
                sfxAudio[0].Play();
            }
        }
    }
}
