using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class TextEvent : MonoBehaviour
{
    [SerializeField] private DialogueUIController DefaultDialogueController;
    [SerializeField] private DialogueUIController ShopdialogueController;
    [SerializeField] private DialogueUIController NoCoinDialogueController;

    public void ShowStartText()
    {
        DefaultDialogueController.ShowDialogueUI();
    }

    public void NoCoinText()
    {
        ShopdialogueController.HideDialogueUI();
        NoCoinDialogueController.ShowDialogueUI();
    }

    public void ShowShopText()
    {
        NoCoinDialogueController.HideDialogueUI();
        ShopdialogueController.ShowDialogueUI();
    }

    public void FreezePlayer()
    {
        PlayerController.Instance.isFreeze = true;
    }

    public void UnFreezePlayer()
    {
        PlayerController.Instance.isFreeze = false;
    }
}
