using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class IntroEvent : MonoBehaviour
{
    [SerializeField] private DialogueUIController DefaultDialogueController;
    void Start()
    {
        DefaultDialogueController.ShowDialogueUI();
    }

    void Update()
    {
        
    }
}
