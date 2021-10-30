using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpriteSwitcher : MonoBehaviour
{
    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public Sprite dayArt;
    [HorizontalGroup("Split", Width = 50), HideLabel, PreviewField(50)]
    public Sprite nightArt;
    

    public void Start()
    {
        if (TimeManagement.Instance.isNight())
            GetComponent<SpriteRenderer>().sprite = nightArt;
        else
            GetComponent<SpriteRenderer>().sprite = dayArt;
    }

}