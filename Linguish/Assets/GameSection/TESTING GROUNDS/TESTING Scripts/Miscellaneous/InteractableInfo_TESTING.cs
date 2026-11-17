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
            npcInteractable = GetComponentInParent<NPCInterectable_TESTING>();
        }


        npcData = npcInteractable.Data;
        
    }

    public string GetInteractableName()
    {
        if (interactableData != null)
        {
            return interactableData.interactableName;
        }
        
        if (npcData != null)
        {
            return npcData.npcName;
        }
        return gameObject.name; // Fallback
    }
}
