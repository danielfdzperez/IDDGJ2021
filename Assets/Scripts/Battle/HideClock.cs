using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class HideClock : MonoBehaviour
{
    CircleCollider2D collider;
    [SerializeField]
    Image clock;
    [SerializeField]
    Image arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerCombat player = collision.GetComponent<PlayerCombat>();
        if(player)
        {
            Debug.Log("Entra");
            clock.color = new Color(1,1,1,0.2f);
            arrow.color = new Color(1, 1, 1, 0.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerCombat player = collision.GetComponent<PlayerCombat>();
        if (player)
        {
            clock.color = new Color(1, 1, 1, 1);
            arrow.color = new Color(1, 1, 1, 1);
        }
    }
}
