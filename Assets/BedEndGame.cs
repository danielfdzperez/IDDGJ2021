using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BedEndGame : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;
    // Start is called before the first frame update
    Dialog currentDialog;


    [SerializeField]
    GameObject mark;

    [SerializeField]
    SpriteRenderer taskSprite;

    bool completed;
    void Start()
    {
        if (canvas == null)
            Debug.LogError("Task no tiene canvas asociado");
        canvas.enabled = false;

        currentDialog = GetComponent<Dialog>();


        if (!TimeManagement.Instance.isNight())
        {

            mark.SetActive(false);
            completed = true;
        }
        else
        {
            completed = false;
        }
    }



    private void ShowCanvas(bool show)
    {
        //Luego se llamara a las animaciones, pero ahora no hay nada
        if (show)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject player = collision.gameObject.GetComponent<MovementComponent>() != null ? collision.gameObject : null;
        if (player && !completed)
        {

            collision.gameObject.GetComponent<PlayerTaskHandler>().SetCurrentDialog(true, currentDialog);
            ShowCanvas(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject player = collision.gameObject.GetComponent<MovementComponent>() != null ? collision.gameObject : null;

        if (player && !completed)
        {

            collision.gameObject.GetComponent<PlayerTaskHandler>().SetCurrentDialog(false, null);
            ShowCanvas(false);
        }
    }

}

