using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Boss : MonoBehaviour, HittableInterface
{
    [SerializeField]
    [Range(0,1)]
    float damageByPerfetHit;
    [SerializeField]
    [Range(0, 1)]
    float damageByNonPerfectHit;

    float currentLife = 1;

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    public FloatEvent OnHit;
    private void Start()
    {
        if (OnHit == null)
            OnHit = new FloatEvent();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Proyectile proyectile = collision.GetComponent<Proyectile>();
    //    if(proyectile)
    //    {

    //    }
    //}

    public void Hit(HitType hitType)
    {
        switch(hitType)
        {
            case HitType.perfect:
                currentLife -= damageByPerfetHit;
                break;
            case HitType.nonPerfect:
                currentLife -= damageByNonPerfectHit;
                break;
        }
        currentLife = Mathf.Max(currentLife, 0);
        OnHit.Invoke(currentLife);
    }
}
