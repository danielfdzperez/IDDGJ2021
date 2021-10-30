using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour, HittableInterface
{
    List<GameObject> posibleTargets = new List<GameObject>();

    GameObject currentTarget;

    [SerializeField]
    [Range(0,1)]
    float damageByMissOrHit = 0.1f;

    float life = 1;

    bool died = false;

    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

    [Header("Events")]
    [Space]

    public GameObjectEvent OnSelectTarget;
    public UnityEvent OnNoTargets;
    public UnityEvent OnDie;


    // Start is called before the first frame update
    void Start()
    {
        if (OnSelectTarget == null)
            OnSelectTarget = new GameObjectEvent();
        if (OnNoTargets == null)
            OnNoTargets = new UnityEvent();
        if (OnDie == null)
            OnDie = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget == null && posibleTargets.Count > 0 && !died)
        {
            SelectNewTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (died)
            return;
        HittableInterface hittable = collision.GetComponent<HittableInterface>();
        if (hittable != null)
        {
            posibleTargets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (died)
            return;
        HittableInterface hittable = collision.GetComponent<HittableInterface>();
        if (hittable != null)
        {
            posibleTargets.Remove(collision.gameObject);
            if(collision.gameObject == currentTarget)
            {
                SelectNewTarget();
            }
        }
    }

    void SelectNewTarget()
    {
        GameObject target = null;
        foreach(GameObject obj in posibleTargets)
        {
            if(target == null || target.transform.position.x < obj.transform.position.x)
            {
                target = obj;
            }
        }
        currentTarget = target;
        if (currentTarget)
            OnSelectTarget.Invoke(currentTarget);
        else
            OnNoTargets.Invoke();
    }

    public void HitSomething(HitType hitType)
    {
        switch(hitType)
        {
            case HitType.perfect:
            case HitType.nonPerfect:
                currentTarget.GetComponent<HittableInterface>().Hit(hitType);
                break;
            case HitType.fail:
                Hit(hitType);
                break;
        }
    }

    public void Hit(HitType hitType)
    {
        life -= damageByMissOrHit;

        if(life <= 0)
        {
            //Si se queda sin vida pierde
            Die();
        }
    }

    void Die()
    {
        died = true;
        posibleTargets.Clear();
        OnNoTargets.Invoke();
        OnDie.Invoke();
    }
}
