using UnityEngine;

public class NPCInterectable_TESTING : MonoBehaviour
{

    public NPCDataSO_TESTING Data;
    

    public void Interact()
    {
        if (Data == null)
        {
            Debug.LogWarning("NPC Data is not assigned for " + gameObject.name);
            return;
        }
        Debug.Log("Dialogue for " + Data.npcName + ": " + Data.npcDialogue);
        Debug.Log("Wooo there Jamal dont pull out the nine");
    }
}
