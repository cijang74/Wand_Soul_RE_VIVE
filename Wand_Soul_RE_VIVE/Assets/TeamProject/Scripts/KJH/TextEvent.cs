using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;
using TMPro;

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

    public void SetGoldUI()
    {
        GameObject goldCoinContainer = GameObject.Find("UI_Canvas").transform.Find("Gold Coin Container").gameObject;

        if (goldCoinContainer != null)
        {
            RectTransform rectTransform = goldCoinContainer.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // 현재 위치를 가져와 Y 값을 변경
                Vector3 newPosition = rectTransform.localPosition;
                newPosition.y -= 300; // 원하는 만큼 아래로 이동 (예: -100)
                rectTransform.localPosition = newPosition;
            }
        }
    }

    public void SetDefaultGoldUI()
    {
        GameObject goldCoinContainer = GameObject.Find("UI_Canvas").transform.Find("Gold Coin Container").gameObject;

        if (goldCoinContainer != null)
        {
            RectTransform rectTransform = goldCoinContainer.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // 현재 위치를 가져와 Y 값을 변경
                Vector3 newPosition = rectTransform.localPosition;
                newPosition.y += 300; // 원하는 만큼 아래로 이동 (예: -100)
                rectTransform.localPosition = newPosition;
            }
        }
    }

}
