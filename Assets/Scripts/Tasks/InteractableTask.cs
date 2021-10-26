using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTask : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        if (canvas == null)
            Debug.LogError("Task no tiene canvas asociado");
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowCanvas(bool show)
    {
        //Luego se llamara a las animaciones, pero ahora no hay nada
        if(show)
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
        if (player)
        {
            ShowCanvas(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject player = collision.gameObject.GetComponent<MovementComponent>() != null ? collision.gameObject : null;

        if (player)
        {
            ShowCanvas(false);
        }
    }

}
