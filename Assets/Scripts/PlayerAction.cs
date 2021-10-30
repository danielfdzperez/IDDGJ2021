using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New PlayerAction", menuName = "Action/Player Action")]
public  class PlayerAction : ScriptableObject
{


    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]

    [VerticalGroup("Split/Properties")]
    public string title;
    public Sprite icon;

    public bool isAttack;
    public int Damage;
    public bool isDefense;
    public bool isRun;

}