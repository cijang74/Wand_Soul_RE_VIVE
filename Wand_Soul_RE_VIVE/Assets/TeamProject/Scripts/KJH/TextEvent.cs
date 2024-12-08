using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class TextEvent : MonoBehaviour
{
    [SerializeField] private DialogueUIController dialogueUIController;

    // Start is called before the first frame update
    void Start()
    {
        dialogueUIController.ShowDialogueUI();
    }
}
