using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public partial class AttackBar : MonoBehaviour
{

    public float currentValue { get; private set; }

    [SerializeField]
    float speed = 0.5f;

    [SerializeField]
    float perfectHitThreshold = 0f;

    float perfectHit = 0f;

    float nonPerfectHit = 0f;

    [SerializeField]
    float nonPerfectHitThreshold = 0f;


    bool active = false;


    [Header("Events")]
    [Space]

    public UnityEvent OnEnd;


    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    [System.Serializable]
    public class HitTypeEvent : UnityEvent<HitType> { }

    [System.Serializable]
    public class Float4Event : UnityEvent<float, float, float, float> { }

    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

    public Float4Event OnGenerateHitValues;
    public GameObjectEvent OnActivate;

    public HitTypeEvent OnHit;
    //public HitTypeEvent OnFail;
    public FloatEvent OnUpdatevalue;


    private void Awake()
    {
        if (OnEnd == null)
            OnEnd = new UnityEvent();

        if (OnActivate == null)
            OnActivate = new GameObjectEvent();

        if (OnGenerateHitValues == null)
            OnGenerateHitValues = new Float4Event();

        if (OnHit == null)
            OnHit = new HitTypeEvent();
        //if (OnFail == null)
        //    OnFail = new HitTypeEvent();
        
        if (OnUpdatevalue == null)
            OnUpdatevalue = new FloatEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        //End();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            //currentValue = currentValue + Time.deltaTime * speed;
            //currentValue = Mathf.Min(1, currentValue);
            currentValue = Mathf.MoveTowards(currentValue, 1, Time.deltaTime * speed);
            OnUpdatevalue.Invoke(currentValue);
            if (currentValue == 1)
            {
                currentValue = 0;
                GenerateHitValues();
                //Mal deberia haber un On
                
            }
                
        }
    }

    public void End()
    {
        active = false;
        OnEnd.Invoke();
    }

    public void Activate()
    {
        currentValue = 0f;
        active = true;
        GenerateHitValues();
    }

    void GenerateHitValues()
    {

        perfectHit = Random.Range(0.1f, 0.5f);
        nonPerfectHit = Random.Range(perfectHit+nonPerfectHitThreshold, 1- nonPerfectHitThreshold);
        OnGenerateHitValues.Invoke(perfectHit, perfectHitThreshold, nonPerfectHit, nonPerfectHitThreshold);
    }

    void Action()
    {
        if (!active)
        {
            Activate();
            return;
        }

        if (Mathf.Abs(currentValue - perfectHit) <= perfectHitThreshold)
        {
            OnHit.Invoke(HitType.perfect);
        }
        else if (Mathf.Abs(currentValue - nonPerfectHit) <= nonPerfectHitThreshold)
        {
            OnHit.Invoke(HitType.nonPerfect);
        }
        else
        {
            OnHit.Invoke(HitType.fail);
        }

        //End();
    }

    public void PressButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Action();
        }

    }

    public void GetHitPositionsInfo(out float perfectHit, out float perfectHitThreshold, out float nonPerfectHit, out float nonPerfectHitThreshold)
    {
        perfectHit = this.perfectHit;
        perfectHitThreshold = this.perfectHitThreshold;
        nonPerfectHit = this.nonPerfectHit;
        nonPerfectHitThreshold = this.nonPerfectHitThreshold;
    }
}
