using UnityEngine;
using TMPro;
using System;
using System.Data.Common;

public class DialogueUI_TESTING : MonoBehaviour
{

    [SerializeField] private GameObject containerDialogueUI;
    // [SerializeField] private PlayerInterection_TESTING playerInteraction; //There is a keybind option here, for advancing through dialogue

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI interactableName; //Named it this cause not sure if we want other things beside NPC to have Dialogue, like a door can have a quick short dialogue being like "Sealed from the inside"

    void Awake()
    {
        if (containerDialogueUI == null)
        {
            Debug.LogError("Dialogue UI Container ain't assigned");
            enabled = false;
            return;
        }

        if (dialogueText == null)
        {
            Debug.LogError("Dialogue Text ain't assigned");
            enabled = false;
            return;
        }

        if (interactableName == null)
        {
            Debug.LogError("Interactable Name ain't assigned");
            enabled = false;
            return;
        }
        Hide();
    }

    private void Show()
    {
        containerDialogueUI.SetActive(true);
    }

    private void Hide()
    {
        containerDialogueUI.SetActive(false);
    }

    public bool IsDialogueOpen() //This is going to help to avoid multiple dialogues clashing with one another
    {
        if (containerDialogueUI == null)
        {
            return false;
        }

        if (containerDialogueUI.activeInHierarchy == true)
        {
            return true;
        }

        return false;
    }

    public void ShowDialogue(string name, string dialogue)
    {

        if (string.IsNullOrWhiteSpace(dialogue))
        {
            return;
        }

        if (containerDialogueUI.activeSelf == true)
        {
            return;
        }


        if (IsDialogueOpen () == true)
        {
            return;
        }

        interactableName.text = name;

        dialogueText.text = dialogue;

        Show();
        Debug.Log("DIALOGUE STARTED");
    }

    public void CloseDialogue()
    {
        Hide();
        Debug.Log("DIALOGUE ENDED");
    }

    void Update()
    {

    }
}
