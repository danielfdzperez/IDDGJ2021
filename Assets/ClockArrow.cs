using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeManagement.Instance.UpdateClock(GetComponent<RectTransform>());
    }

  
}
