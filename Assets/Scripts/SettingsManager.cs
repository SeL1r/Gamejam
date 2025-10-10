using System;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{ 

    public class SettingsManager : MonoBehaviour
    {
        private Camera cam;
        public Button cancel, apply;
        SettingsData settingsData = new SettingsData();
HEAD

        [Header("Настройки")]

70e629b3ec6a30f10a516ad270e0e7bfc8d33aa9
        public Scrollbar scrollbarSensetivity, fovScrollbar, MVScrollbar;
        public TMP_Text SensetivityValue, fovText, MVText;

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
            
        }
HEAD
        //кнопка отмены


70e629b3ec6a30f10a516ad270e0e7bfc8d33aa9
        void CancelChanges()
        {
            LoadSettings();
        }
HEAD
        //кнопка принять


70e629b3ec6a30f10a516ad270e0e7bfc8d33aa9
        void ApplyChanges()
        {
            
            SaveSettings();
        }

        void SaveSettings()
        {
HEAD
            //сохраняем свойства

70e629b3ec6a30f10a516ad270e0e7bfc8d33aa9
            settingsData.sensetivity = (scrollbarSensetivity.value * 9) + 1;
            PlayerPrefs.SetFloat("Sensetivity", settingsData.sensetivity);
            settingsData.fieldOfView = (fovScrollbar.value * 60) + 30;
            PlayerPrefs.SetFloat("FOV", settingsData.fieldOfView); 
            cam.fieldOfView = settingsData.fieldOfView;
            settingsData.masterVolume = (MVScrollbar.value * 100);
            PlayerPrefs.SetFloat("MasterVolume", settingsData.masterVolume);
        }
        void LoadSettings()
        {
HEAD
            //берем предыдущие данные

70e629b3ec6a30f10a516ad270e0e7bfc8d33aa9
            float prevSense = PlayerPrefs.GetFloat("Sensetivity");
            float prevFOV = PlayerPrefs.GetFloat("FOV");
            float prevMV = PlayerPrefs.GetFloat("MasterVolume");

            scrollbarSensetivity.value = prevSense / 9;
            fovScrollbar.value = (prevFOV - 30) / 60;
            MVScrollbar.value = prevMV / 100;

            
        }
    }

    [Serializable]
    public class SettingsData
    {
        public float sensetivity = 3f;
        public float fieldOfView = 60;
        public  bool isFullScreen = true;

        public float masterVolume = 50;
        public float musicVolume = 50;
        public float effectsVolume = 50;
    }

}
