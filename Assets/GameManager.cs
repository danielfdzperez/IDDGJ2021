using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update

    [TableList(AlwaysExpanded = true, DrawScrollView = false)]
    public List<SimpleDialog> AlwaysExpandedTable = new List<SimpleDialog>();
    public int score;



    public List<EnemyTask> enemyTaskTable = new List<EnemyTask>();
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            if(SoundManager.Instance!=null)
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
        if (!SceneManager.GetActiveScene().name.Equals("Ending"))
        {
          
            foreach (SimpleDialog dialog in AlwaysExpandedTable)
            {
                if (dialog.timeToShow == TimeManagement.Instance.currentTime)
                {
                    FindObjectOfType<PlayerTaskHandler>().ActivateDialog(dialog.sentences);

                }
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


    public void playtext() {

        if (TimeManagement.Instance.currentTime >= 6)
            SceneManager.LoadScene("Ending");

        foreach (SimpleDialog dialog in AlwaysExpandedTable)
        {

            if (dialog.timeToShow == TimeManagement.Instance.currentTime)
            {
                FindObjectOfType<PlayerTaskHandler>().ActivateDialog(dialog.sentences);

            }
        }

    }


    public void handleEnding() {

        
        score = PlayerPrefs.GetInt("Loza sucia", 0)+
                PlayerPrefs.GetInt("Argos el perro flojo", 0)+
                PlayerPrefs.GetInt("Abuela Eustaquia", 0)+
                PlayerPrefs.GetInt("TV PG poltergeist", 0);

        if (score > 4)
            score = 4;
        foreach (SimpleDialog dialog in AlwaysExpandedTable)
        {

            if (dialog.timeToShow == 6 && dialog.scoreNeeded ==score)
            {
                FindObjectOfType<PlayerTaskHandler>().ActivateDialog(dialog.sentences);
                
            }
        }
        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>().text="Tareas: "+score+" de 4" ;
    }
}

