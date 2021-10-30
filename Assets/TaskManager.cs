using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    [SerializeField]
    EnemyTask currentTask;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
    }




    public void SetCurrentTask(EnemyTask enemyTask)
    {
        currentTask = enemyTask;
    }

    public EnemyTask GetCurrentTask() {
        return currentTask;
    }
}
