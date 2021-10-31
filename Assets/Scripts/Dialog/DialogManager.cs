using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    GameObject dialogObj;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    GameObject yesButton;
    [SerializeField]
    GameObject noButton;
    [SerializeField]
    GameObject continueButton;

    public static DialogManager instance;

    Dialog currentDialog;

    private void Awake()
    {
        instance = this;
        //EndDialog();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Comprobar que esta todo


    }

    public void DisplayNewDialog(Dialog dialog)
    {
        currentDialog = dialog;
        dialogObj.SetActive(true);
    }

    public void EndDialog()
    {
        dialogObj.SetActive(false);
        text.text = "";
        ShowHideYesButton(false);
        ShowHideNoButton(false);
        ShowHideContinueButton(false);
    }

    public void SetText(string dialog)
    {
        text.text = dialog;
    }

    public void AddLetter(char letter)
    {
        text.text += letter;
    }

    public void ShowHideYesButton(bool show)
    {
        //Aqui por si se quieren hacer animaciones
        if(show)
        {
            yesButton.SetActive(true);
        }
        else
        {
            yesButton.SetActive(false);
        }
    }

    public void ShowHideNoButton(bool show)
    {
        //Aqui por si se quieren hacer animaciones
        if (show)
        {
            noButton.SetActive(true);
        }
        else
        {
            noButton.SetActive(false);
        }
    }

    public void ShowHideContinueButton(bool show)
    {
        //Aqui por si se quieren hacer animaciones
        if (show)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void OnYes()
    {
        currentDialog.Answer(true);
    }

    public void OnFalse()
    {
        currentDialog.Answer(false);
    }

    public void OnContinue()
    {
        currentDialog.NextSentence();
    }
}
