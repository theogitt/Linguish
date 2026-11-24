using UnityEngine;

public class NPCInterectable_TESTING : MonoBehaviour
{

    public NPCDataSO_TESTING Data;

    private bool isTalking = false;
    

    public void Interact()
    {
        if (Data == null)
        {
            Debug.LogWarning("NPC Data is not assigned for " + gameObject.name);
            return;
        }

        if (string.IsNullOrEmpty(Data.npcName))
        {
            Debug.LogWarning("NPC Name is not assigned in the data for " + gameObject.name);
            return;
        }

        if (string.IsNullOrEmpty(Data.npcDialogue))
        {
            Debug.LogWarning("NO NPC DIALOGUE ASSIGNED FOR " + gameObject.name);
            return;
        }

        Debug.Log("Dialogue for " + Data.npcName + ": " + Data.npcDialogue);


        // ADD DIALOGUE SYSTEM/UI LOGIC HERE, also set isTalking to true when dialogue starts and false when it ends 
    }
}
