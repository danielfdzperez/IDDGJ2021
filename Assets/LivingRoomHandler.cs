using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivingRoomHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        int score = PlayerPrefs.GetInt("Loza sucia", 0) +
                PlayerPrefs.GetInt("Argos el perro flojo", 0) +
                PlayerPrefs.GetInt("Abuela Eustaquia", 0) +
                PlayerPrefs.GetInt("TV PG poltergeist", 0);
        if (score == 4 || TimeManagement.Instance.currentTime>=6)
            SceneManager.LoadScene("Ending");

       /* if (TimeManagement.Instance.currentTime == 3)
            GameManager.Instance.playtext();*/
    }

   
}
