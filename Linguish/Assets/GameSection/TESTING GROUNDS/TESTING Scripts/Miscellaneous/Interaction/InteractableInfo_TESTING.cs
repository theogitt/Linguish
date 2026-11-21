using UnityEngine;

public class InteractableInfo_TESTING : MonoBehaviour
{
    public InteractableSO_TESTING interactableData;
    public NPCDataSO_TESTING npcData;
    public NPCInterectable_TESTING npcInteractable;

    void Start()
    {

        if (npcInteractable == null)
        {
            Debug.LogWarning("NPC Interactable is not assigned for " + gameObject.name + ". Prob forgot to assign it. Attempting to find it on the same GameObject.");

            npcInteractable = GetComponent<NPCInterectable_TESTING>();

            if (npcInteractable != null)
            {
                Debug.Log("Found NPC Interactable on " + gameObject.name);
            }
            else
            {
                Debug.LogWarning("GG go next. Could not find NPC Interactable on " + gameObject.name);
            }
        }
        else
        {
            npcData = npcInteractable.Data;

            if (npcData == null)
            {
                Debug.LogWarning("NPC Data is not assigned for " + gameObject.name);
            }
        }

    }

    public string GetInteractableName()
    {
        if (interactableData != null)
        {
            if (interactableData.interactableType == InteractableType.NPC)
            {
                if (!string.IsNullOrEmpty(npcData.npcName))
                {
                    return npcData.npcName;
                }

                else
                {
                    Debug.LogWarning("NPC Name is not assigned for " + gameObject.name);
                }
            }
        }

        return gameObject.name;
    }
}
