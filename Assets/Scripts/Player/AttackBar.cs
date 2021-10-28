using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AttackBar : MonoBehaviour
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

    [SerializeField]
    float perfectDamage = 10f;

    [SerializeField]
    float nonPerfectDamage = 2f;

    [SerializeField]
    float hitByMiss = 2f;

    bool active = false;


    [Header("Events")]
    [Space]

    public UnityEvent OnEnd;


    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    [System.Serializable]
    public class Float4Event : UnityEvent<float, float, float, float> { }
    public Float4Event OnActivate;

    public FloatEvent OnHit;
    public FloatEvent OnFail;
    public FloatEvent OnUpdatevalue;


    private void Awake()
    {
        if (OnEnd == null)
            OnEnd = new UnityEvent();

        if (OnActivate == null)
            OnActivate = new Float4Event();

        if (OnHit == null)
            OnHit = new FloatEvent();
        if (OnFail == null)
            OnFail = new FloatEvent();
        
        if (OnUpdatevalue == null)
            OnUpdatevalue = new FloatEvent();
    }

    // Start is called before the first frame update
    void Start()
    {

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
                End();
        }
    }

    void End()
    {
        active = false;
        OnEnd.Invoke();
    }

    void Activate()
    {
        currentValue = 0f;
        active = true;
        GenerateHitValues();
        OnActivate.Invoke(perfectHit, perfectHitThreshold,nonPerfectHit,nonPerfectHitThreshold);
    }

    void GenerateHitValues()
    {
        perfectHit = Random.Range(0.1f, 0.5f);
        nonPerfectHit = Random.Range(perfectHit+perfectHitThreshold, 1-nonPerfectHitThreshold);
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
            OnHit.Invoke(perfectDamage);
        }
        else if (Mathf.Abs(currentValue - nonPerfectHit) <= nonPerfectHitThreshold)
        {
            OnHit.Invoke(nonPerfectDamage);
        }
        else
        {
            OnFail.Invoke(hitByMiss);
        }

        End();
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
