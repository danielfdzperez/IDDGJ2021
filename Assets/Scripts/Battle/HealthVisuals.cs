using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisuals : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        if (slider == null)
            Debug.LogError("No hay slider para la parte visual de la vida");
    }

    public void UpdateSlider(float health)
    {
        slider.value = health;
    }
}
