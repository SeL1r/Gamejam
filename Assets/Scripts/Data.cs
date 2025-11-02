using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Data")]
public class Data : ScriptableObject
{
    [SerializeField]
    private int i = 0;
    [SerializeField]
    private int currKey = 1;
    public bool readyToOpen = false;
    public List<string> subtitles = new List<string>();
    public List<int> keyI = new List<int>();
    
    public void NextSubtitle(TextMeshPro tmp, AudioSource audioSource)
    {
        if (i < keyI[currKey])
        {
            tmp.text = subtitles[i];

            i++;
        }
        else if(i == keyI[currKey])
        {
            tmp.text = "Do you want to repeat? Sure.";
            i = keyI[currKey - 1];
        }
        audioSource.Play();
    }

    public void NextStage()
    {
        i = keyI[currKey];
        currKey++;
    }

    public void SwichIsReadyToOpen()
    {
        readyToOpen = !readyToOpen;
    }





    
}
