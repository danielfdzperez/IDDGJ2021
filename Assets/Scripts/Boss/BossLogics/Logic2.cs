using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic2 : LogicBase
{
    [SerializeField]
    GameObject proyectilePrefab;

    [SerializeField]
    Transform minPos;
    [SerializeField]
    Transform maxPos;

    [SerializeField]
    GameObject player;

    [SerializeField]
    AudioSource shotAudio;

    Func<IEnumerator> CurrentState = null;

    Coroutine currentStateRoutine;
    // Start is called before the first frame update
    void Start()
    {
        if (proyectilePrefab.GetComponent<Projectile>() == null)
            Debug.LogError("La logica del boss no tiene el prefab del proyectil");
        CurrentState = State1;
        //StartCoroutine(State1());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator State1()
    {
        while (true)
        {
            float split = 0;

            for (int i = 0; i < 30; i++)
            {
                split = UnityEngine.Random.Range(minPos.position.y, maxPos.position.y);
                Vector2 pos = new Vector2(maxPos.position.x, split);
                GameObject projecitle = Instantiate(proyectilePrefab, transform.position, Quaternion.identity); ;
                //Debug.Log(projecitle.GetComponent<Projectile>());
                //projecitle.GetComponent<Projectile>().SetDirectionAndSpeed(direction, 5);
                //Vector2 dir = ((Vector2)transform.position - pos) * -1;
                Vector2 dir = (pos - (Vector2)transform.position);
                Projectile p = projecitle.GetComponent<Projectile>();
                p.SetDirectionAndSpeed(dir, 10);
                p.bossPosition = transform.position;
                SoundManager.Instance.PlaySFX(shotAudio);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }

    }

    IEnumerator State2()
    {
        while (true)
        {
            float split = 0;

            for (int j = 0; j < 30; j++)
            {
                split = UnityEngine.Random.Range(minPos.position.y, maxPos.position.y);
                for (int i = 0; i < 3; i++)
                {
                    float spread = UnityEngine.Random.Range(-3f, 3f);
                    Vector2 pos = new Vector2(player.transform.position.x, player.transform.position.y + spread);
                    GameObject projecitle = Instantiate(proyectilePrefab, transform.position, Quaternion.identity); ;
                    //Debug.Log(projecitle.GetComponent<Projectile>());
                    //projecitle.GetComponent<Projectile>().SetDirectionAndSpeed(direction, 5);
                    //Vector2 dir = ((Vector2)transform.position - pos) * -1;
                    Vector2 dir = (pos - (Vector2)transform.position);
                    Projectile p = projecitle.GetComponent<Projectile>();
                    p.SetDirectionAndSpeed(dir, 8);
                    p.bossPosition = transform.position;
                    SoundManager.Instance.PlaySFX(shotAudio);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(1f);
        }

    }

    public void BossHit(float health)
    {
        if(health <= 0.7 && CurrentState != State2)
        {
            CurrentState = State2;
        }
    }

    public override void Activate(bool activate)
    {
        if (activate)
        {
            currentStateRoutine = StartCoroutine(CurrentState());
        }
        else
        {
            if (currentStateRoutine != null)
                StopCoroutine(currentStateRoutine);
        }
    }
}
