using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;
    [SerializeField]
    bool isEnding;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isEnding)
            Switch();
        
    }

    public void Switch()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
