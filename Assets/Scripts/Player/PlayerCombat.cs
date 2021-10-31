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

    float health = 1;

    bool died = false;

    [SerializeField]
    GameObject weapon;
    Coroutine weaponAnim;

    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }
    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }



    [Header("Events")]
    [Space]

    public GameObjectEvent OnSelectTarget;
    public UnityEvent OnNoTargets;
    public UnityEvent OnDie;
    public FloatEvent OnHit;


    // Start is called before the first frame update
    void Start()
    {
        if (OnSelectTarget == null)
            OnSelectTarget = new GameObjectEvent();
        if (OnNoTargets == null)
            OnNoTargets = new UnityEvent();
        if (OnDie == null)
            OnDie = new UnityEvent();
        if (OnHit == null)
            OnHit = new FloatEvent();
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

        List<GameObject> l = new List<GameObject>();
        foreach (GameObject obj in posibleTargets)
        {
            if (obj == null)
                l.Add(obj);
        }
        foreach (GameObject obj in l)
        {
            posibleTargets.Remove(obj);
        }

        foreach (GameObject obj in posibleTargets)
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
        {
            Debug.Log("No target");
            OnNoTargets.Invoke();
        }
    }

    public void HitSomething(HitType hitType)
    {
        switch(hitType)
        {
            case HitType.perfect:
            case HitType.nonPerfect:
                if (weapon)
                {
                    if(weaponAnim != null)
                        StopCoroutine(weaponAnim);
                    weaponAnim = StartCoroutine(ShowWeapon());
                }
                currentTarget.GetComponent<HittableInterface>().Hit(hitType);
                break;
            case HitType.fail:
                Hit(hitType);
                break;
        }
    }

    public void Hit(HitType hitType)
    {
        health -= damageByMissOrHit;
        OnHit.Invoke(health);
        if(health <= 0)
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

    IEnumerator ShowWeapon()
    {
        weapon.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        weapon.SetActive(false);
    }
}
