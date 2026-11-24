using UnityEngine;

public class NPCInterectable_TESTING : MonoBehaviour
{

    public NPCDataSO_TESTING Data;

    [SerializeField] private DialogueUI_TESTING dialogueUI;

    // private bool isTalking = false;


    void Start()
    {
        if (dialogueUI == null)
        {
            dialogueUI = FindAnyObjectByType<DialogueUI_TESTING>();
        }
    }

    public void Interact()
    {
        if (Data == null)
        {
            Debug.LogWarning("NPC Data is not assigned for " + gameObject.name);
            return;
        }

        if (dialogueUI == null)
        {
            dialogueUI = FindAnyObjectByType<DialogueUI_TESTING>();
        }


        if (dialogueUI == null)
        {
            return;
        }

        string npcName = Data.name;


        if (string.IsNullOrEmpty(npcName))
        {
            npcName = gameObject.name;
        }

        // ADD DIALOGUE SYSTEM/UI LOGIC HERE, also set isTalking to true when dialogue starts and false when it ends 
        dialogueUI.ShowDialogue(Data.npcName, Data.npcDialogue);
        Debug.Log("Dialogue for " + Data.npcName + ": " + Data.npcDialogue);
    }
}
