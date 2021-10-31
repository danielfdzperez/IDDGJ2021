using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTaskHandler : MonoBehaviour
{
    [SerializeField]
    MovementComponent movementComponent;

    public bool canActivateDialog;
    [SerializeField]
    Dialog currentDialog;


    //Action Maps
    private string actionMapPlayerControls = "Player";
    private string actionMapMenuControls = "UI";


    public PlayerInput playerInput;/*
    private PlayerController focusedPlayerController;*/


  /*  public void Activate() {

     /*   if (movementComponent.block)
        {
            DialogManager.instance.OnContinue();
            return;
        }*/
     /* if (canActivateDialog && currentDialog != null)
        {
            EnablePauseMenuControls();
           // movementComponent.ToggleBlock();
            currentDialog.Activate();
         }
    }*/

    public void SetCurrentDialog(bool enable, Dialog dialog) {

        canActivateDialog = enable;
        currentDialog = dialog;
    }



    

    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
    }

    public void EnablePauseMenuControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    }


    public void Activate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canActivateDialog && currentDialog != null)
            {
                EnablePauseMenuControls();
                // movementComponent.ToggleBlock();
                currentDialog.Activate();
            }
        }
    }

    public void ActivateDialog(string[] texts)
    {
        currentDialog.UpdateSentences(texts);
           
                EnablePauseMenuControls();
                // movementComponent.ToggleBlock();
                currentDialog.Activate();
            
    }
}
