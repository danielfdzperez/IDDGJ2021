using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    public EnemyTask taskDefinition;

    [SerializeField]
    string[] sentences;
    int sentenceIndex = 0;
    [SerializeField]
    float speed;
    [SerializeField]
    bool wihtAnswer = false;
    [SerializeField]
    bool canBeRepeated = true;


    [Header("Events")]
    [Space]

    public UnityEvent OnYes;
    public UnityEvent OnNo;
    public UnityEvent OnEnd;
    public UnityEvent OnStart;

    public bool active = false;

    private void Start()
    {
        if(taskDefinition!=null)
            sentences = taskDefinition.sentences;

        if (OnYes == null)
            OnYes = new UnityEvent();
        if (OnNo == null)
            OnNo = new UnityEvent();
        if (OnEnd == null)
            OnEnd = new UnityEvent();
        if (OnStart == null)
            OnStart = new UnityEvent();
    }
    public void Activate()
    {
        if (active)
            return;
        active = true;
        sentenceIndex = 0;
        DialogManager.instance.DisplayNewDialog(this);
        StartCoroutine(Type());
        OnStart.Invoke();
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[sentenceIndex].ToCharArray())
        {
            DialogManager.instance.AddLetter(letter);
            yield return new WaitForSeconds(speed);
        }

        if (! (sentenceIndex < sentences.Length - 1) && wihtAnswer) {
            DialogManager.instance.ShowHideYesNoPrompt(true);
            DialogManager.instance.ShowHideYesButton(true);
            DialogManager.instance.ShowHideNoButton(true);
        }
        else
        {
            DialogManager.instance.ShowHideContinueButton(true);
        }
    }

    public void NextSentence()
    {
        if(sentenceIndex < sentences.Length-1)
        {
            sentenceIndex++;
            DialogManager.instance.SetText("");
            DialogManager.instance.ShowHideContinueButton(false);
            StartCoroutine(Type());
        }
        else {
            CloseDialog();
        }
    }

    public void Answer(bool yes)
    {
        CloseDialog();
        if(yes)
        {
            OnYes.Invoke();
        }
        else
        {
            OnNo.Invoke();
        }
    }

    void CloseDialog()
    {
        active = false;
        sentenceIndex = 0;
        OnEnd.Invoke();
        if(canBeRepeated)
            active = false;
        DialogManager.instance.EndDialog();
        
    }

    public void SetSentences(string [] newSentences)
    {
        sentences = newSentences;
    }
}
