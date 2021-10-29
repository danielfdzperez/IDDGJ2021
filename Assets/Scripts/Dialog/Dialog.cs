using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{

    [SerializeField]
    string[] sentences;
    int sentenceIndex = 0;
    [SerializeField]
    float speed;
    [SerializeField]
    bool wihtAnswer = false;


    [Header("Events")]
    [Space]

    public UnityEvent OnYes;
    public UnityEvent OnNo;

    bool active = false;

    private void Start()
    {
        if (OnYes == null)
            OnYes = new UnityEvent();
        if (OnNo == null)
            OnNo = new UnityEvent();
    }
    public void Activate()
    {
        if (active)
            return;
        active = true;
        DialogManager.instance.DisplayNewDialog(this);
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[sentenceIndex].ToCharArray())
        {
            DialogManager.instance.AddLetter(letter);
            yield return new WaitForSeconds(speed);
        }

        if (! (sentenceIndex < sentences.Length - 1) && wihtAnswer) {
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
        DialogManager.instance.EndDialog();
    }
}
