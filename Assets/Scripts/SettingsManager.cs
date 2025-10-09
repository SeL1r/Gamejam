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

        SettingsData settingsData = new SettingsData();
        public Scrollbar scrollbarSensetivity;
        public TMP_Text SensetivityValue;

        private void Start()
        {
            settingsData.sensetivity = PlayerPrefs.GetFloat("Sensetivity");
        }

        private void Update()
        {
            PlayerPrefs.SetFloat("Sensetivity", settingsData.sensetivity);
            settingsData.sensetivity = scrollbarSensetivity.value * 5;
            SensetivityValue.text = "" + Mathf.Clamp(settingsData.sensetivity, 1, 5);
            
            
        }
    }

    [Serializable]
    public class SettingsData
    {
        public float sensetivity = 3f;
        public float fieldOfView = 30;
        public  bool isFullScreen = true;

        public float masterVolume = 50;
        public float musicVolume = 50;
        public float effectsVolume = 50;
    }

}
