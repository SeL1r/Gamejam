using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Data")]
public class Data : ScriptableObject
{
    [SerializeField]
    private List<string> subtitles = new List<string>();
    public void NextSubtitle(TextMeshPro tmp, int i)
    {
        if (i < subtitles.Count)
        {
            tmp.text = subtitles[i];
        }
    }
}
