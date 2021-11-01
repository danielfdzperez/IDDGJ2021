using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        PlayerPrefs.SetInt("Loza sucia", 0);
        PlayerPrefs.SetInt("Argos el perro flojo", 0);
        PlayerPrefs.SetInt("Abuela Eustaquia", 0);
        PlayerPrefs.SetInt("TV PG poltergeist", 0);
        SceneManager.LoadScene("HouseTest");

            TimeManagement.Instance.NewGame();

    }


    public void Quit()
    {
        Application.Quit();
    }
}
