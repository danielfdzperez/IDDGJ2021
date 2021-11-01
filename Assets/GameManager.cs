using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update

    [TableList(AlwaysExpanded = true, DrawScrollView = false)]
    public List<SimpleDialog> AlwaysExpandedTable = new List<SimpleDialog>();
    public int score;



    public List<EnemyTask> enemyTaskTable= new List<EnemyTask>();
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            SoundManager.Instance.PlayHouseMusic();
            resetTasks();
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        foreach (SimpleDialog dialog in AlwaysExpandedTable)
        {
            if (dialog.timeToShow == TimeManagement.Instance.currentTime)
            {
                FindObjectOfType<PlayerTaskHandler>().ActivateDialog(dialog.sentences);

            }
        }
    }
    public void AddScore()
    {

        score++;
    }

    void resetTasks() {



        foreach (EnemyTask enemyTask in enemyTaskTable)
        {
            enemyTask.completed = false;
        }
    }
}

