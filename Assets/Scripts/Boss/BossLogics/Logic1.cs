using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic1 : LogicBase
{
    [SerializeField]
    GameObject proyectilePrefab;

    [SerializeField]
    Transform minPos;
    [SerializeField]
    Transform maxPos;

    [SerializeField]
    AudioSource shotAudio;

    Coroutine currentState;
    // Start is called before the first frame update
    void Start()
    {
        if (proyectilePrefab.GetComponent<Projectile>() == null)
            Debug.LogError("La logica del boss no tiene el prefab del proyectil");

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
            float split = Vector2.Distance(minPos.position, maxPos.position) / 10;
            for (int i = 0; i < 10; i++)
            {
                Vector2 pos = new Vector2(transform.position.x, minPos.position.y + 1 * i * split);
                GameObject projecitle = Instantiate(proyectilePrefab, pos, Quaternion.identity); ;
                //Debug.Log(projecitle.GetComponent<Projectile>());
                //projecitle.GetComponent<Projectile>().SetDirectionAndSpeed(direction, 5);
                Projectile p = projecitle.GetComponent<Projectile>();
                p.SetDirectionAndSpeed(Vector2.right * -1, 10);
                p.bossPosition = transform.position;
                
            }
            SoundManager.Instance.PlaySFX(shotAudio);
            yield return new WaitForSeconds(1f);

            split = Vector2.Distance(minPos.position, maxPos.position) / 10;
            for (int i = 0; i < 10; i++)
            {
                Vector2 pos = new Vector2(maxPos.position.x, minPos.position.y + 1 * i * split);
                GameObject projecitle = Instantiate(proyectilePrefab, transform.position, Quaternion.identity); ;
                //Debug.Log(projecitle.GetComponent<Projectile>());
                //projecitle.GetComponent<Projectile>().SetDirectionAndSpeed(direction, 5);
                //Vector2 dir = ((Vector2)transform.position - pos) * -1;
                Vector2 dir = (pos - (Vector2)transform.position);
                Projectile p = projecitle.GetComponent<Projectile>();
                p.SetDirectionAndSpeed(dir, 10);
                p.bossPosition = transform.position;
                SoundManager.Instance.PlaySFX(shotAudio);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
        }

    }

    public override void Activate(bool activate)
    {
        if(activate)
        {
          currentState =  StartCoroutine(State1());
        }
        else
        {
            if (currentState != null)
                StopCoroutine(currentState);
        }
    }
}
