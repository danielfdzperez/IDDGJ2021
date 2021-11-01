using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField]
    AudioSource perfect;
    [SerializeField]
    AudioSource nonPerfect;
    [SerializeField]
    AudioSource fail;
    [SerializeField]
    AudioSource hit;
    [SerializeField]
    AudioSource bossHitSource;
    [SerializeField]
    AudioSource endGameDilog;

    private void Start()
    {
       bossHitSource.clip =   TaskManager.Instance.GetCurrentTask().sfxHurt;
       endGameDilog.clip = TaskManager.Instance.GetCurrentTask().sfxTalk;
    }
    public void AttackBarSFX(HitType type)
    {
        switch(type)
        {
            case HitType.fail:
                SoundManager.Instance.PlaySFX(fail);
                break;
            case HitType.nonPerfect:
                SoundManager.Instance.PlaySFX(nonPerfect);
                break;
            case HitType.perfect:
                SoundManager.Instance.PlaySFX(perfect);
                break;
        }
        
    }

    public void Hit()
    {
        SoundManager.Instance.PlaySFX(hit);
    }

    public void BossHit()
    {
        
        SoundManager.Instance.PlaySFX(bossHitSource);
    }

    public void Dialog()
    {
        SoundManager.Instance.PlaySFX(endGameDilog);
    }
}
