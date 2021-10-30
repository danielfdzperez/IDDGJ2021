using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagement : MonoBehaviour
{
    public static TimeManagement Instance;
    public int currentTime = 0;
    List<float> arrowAngles = new List<float>();
    // Start is called before the first frame update


    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            arrowAngles.Add(180f);
            arrowAngles.Add(123f);
            arrowAngles.Add(58f);
            arrowAngles.Add(0f);
            arrowAngles.Add(-58f);
            arrowAngles.Add(-108.3f);
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
    }




    public void UpdateClock(RectTransform arrow)
    {

        arrow.rotation = Quaternion.Euler(0f, 0f, arrowAngles[currentTime]);
    }

    public bool isNight() {

        return currentTime >= 3;
    }
}
