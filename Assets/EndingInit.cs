using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.handleEnding();
    }

}
