using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "New Simple Dialog", menuName = "Dialog/Simple Dialog")]
public class SimpleDialog : ScriptableObject
{
    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public Sprite dayArt;
    public string dialogName;
    public bool hasArt;
    public string[] sentences;
    public int timeToShow;
    public bool isEnding;
    public int scoreNeeded;
}
