using System;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts
{ 

    public class SettingsManager : MonoBehaviour
    {
        public AudioMixer mixer;
        private Camera cam;
        public Button cancel, apply;
        SettingsData settingsData = new SettingsData();
        public Scrollbar scrollbarSensetivity, fovScrollbar, MVScrollbar, MusicVScrollbar, SFXScrollbar;
        public TMP_Text SensetivityValue, fovText, MVText, SFXText, MusicText;

        private void Start()
        {
            cam = Camera.main;
            apply.onClick.AddListener(ApplyChanges);
            cancel.onClick.AddListener(CancelChanges);
            LoadSettings();
        }

        private void Update()
        {
            SensetivityValue.text = Mathf.Round((scrollbarSensetivity.value * 4) + 1).ToString();
            fovText.text = Mathf.Round((fovScrollbar.value * 60) + 30).ToString();
            MVText.text = Mathf.Round(MVScrollbar.value * 100).ToString();
            MusicText.text = Mathf.Round(MusicVScrollbar.value * 100).ToString();
            SFXText.text = Mathf.Round(SFXScrollbar.value * 100).ToString();
        }
        


        void CancelChanges()
        {
            LoadSettings();
        }

        



        void ApplyChanges()
        {
            
            SaveSettings();
        }

        void SaveSettings()
        { 
            settingsData.sensetivity = (scrollbarSensetivity.value * 9) + 1;
            PlayerPrefs.SetFloat("Sensetivity", settingsData.sensetivity);
            settingsData.fieldOfView = (fovScrollbar.value * 60) + 30;
            PlayerPrefs.SetFloat("FOV", settingsData.fieldOfView); 
            cam.fieldOfView = settingsData.fieldOfView;
            settingsData.masterVolume = (MVScrollbar.value * 100);
            PlayerPrefs.SetFloat("MasterVolume", settingsData.masterVolume);
            settingsData.musicVolume = (MusicVScrollbar.value * 100);
            PlayerPrefs.SetFloat("MusicVolume", settingsData.musicVolume);
            settingsData.effectsVolume = (SFXScrollbar.value * 100);
            PlayerPrefs.SetFloat("SFXVolume", settingsData.effectsVolume);
        }
        void LoadSettings()
        {
            float prevSense = PlayerPrefs.GetFloat("Sensetivity");
            float prevFOV = PlayerPrefs.GetFloat("FOV");
            float prevMV = PlayerPrefs.GetFloat("MasterVolume");
            float prevMusicV = PlayerPrefs.GetFloat("MusicVolume");
            float prevSFXVolume = PlayerPrefs.GetFloat("SFXVolume");

            scrollbarSensetivity.value = prevSense / 9;
            fovScrollbar.value = (prevFOV - 30) / 60;
            MVScrollbar.value = prevMV / 100;
            MusicVScrollbar.value = prevMusicV / 100;
            SFXScrollbar.value = prevSFXVolume / 100;

            
        }
    }

    [Serializable]
    public class SettingsData
    {
        public float sensetivity = 3f;
        public float fieldOfView = 60;
        public  bool isFullScreen = true;

        public float masterVolume = 1;
        public float musicVolume = 1;
        public float effectsVolume = 1;
    }

}
