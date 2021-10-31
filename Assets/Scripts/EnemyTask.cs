using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Task", menuName = "Task/Enemy Task")]
public class EnemyTask : ScriptableObject
{
   public  bool completed;
    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public Sprite dayArt;
    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public Sprite nightArt;

    [VerticalGroup("Split/Properties")]
    public string taskName;

    public int hp;
    public string text;
    public PlayerAction[] playerActions;

    public int attackDamage;
    public string attackDescription;

    public string defeatedString;

    public string[] sentences;
    public string winText;

    public AudioClip battleTheme;
    public AudioClip sfxTalk;
    public AudioClip sfxHurt;
    public Sprite fixedArt;
    public Sprite problemArt;

    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public Sprite defeatedDayArt;
    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public Sprite defeatedNightArt;
    public Sprite GetSprite(bool night)
    {
        if (night)
            return nightArt;
        return dayArt;
    }

    public Sprite GetDefeatedSprite(bool night)
    {
        if (night)
            return defeatedNightArt;
        return defeatedDayArt;
    }
}