using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBarVisuals : MonoBehaviour
{
    [SerializeField]
    Image imagePerfectHit;

    [SerializeField]
    Image imageNonPerfectHit;

    [SerializeField]
    Slider slider;

    float x = 0;
    //AttackBar barLogic;
    // Start is called before the first frame update
    void Start()
    {
        
        //if (barLogic == null)
        //    Debug.LogError("No hay logica de barra de ataque asignada a la parte visual");
    }

    private void Update()
    {
        //x += Time.deltaTime * 0.2f;
        //i.rectTransform.anchorMin = new Vector2(x, 0);
        //i.rectTransform.anchorMax = new Vector2(x, 1);
        //Debug.Log(i.rectTransform.anchoredPosition);
        //i.rectTransform.position = new Vector3(0, i.rectTransform.position.y, i.rectTransform.position.z);
    }

    public void Setup(float perfectHit, float perfectHitThreshold, float nonPerfectHit, float  nonPerfectHitThreshold)
    {
        SetupImage(imagePerfectHit, perfectHit, perfectHitThreshold);
        SetupImage(imageNonPerfectHit, nonPerfectHit, nonPerfectHitThreshold);
    }

    void SetupImage(Image image, float pos, float scale)
    {
        image.transform.localScale = new Vector3(scale, 1, 1);
        image.rectTransform.anchorMin = new Vector2(pos, 0);
        image.rectTransform.anchorMax = new Vector2(pos, 1);
    }

    public void UpdateValue(float value)
    {
        slider.value = value;
    }

}
