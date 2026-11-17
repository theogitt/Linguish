using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionKeyUI_TESTING : MonoBehaviour
{
    [SerializeField] private GameObject containerInteractionKeyUI;
    [SerializeField] private PlayerInterection_TESTING playerInteraction;

    [SerializeField] private TextMeshProUGUI interactionKeyText;
    [SerializeField] private TextMeshProUGUI interactableObjectText;


    private void Start()
    {
        if (containerInteractionKeyUI == null)
        {
            Debug.LogError("Container Interaction Key UI is not assigned in the inspector.");
            return;
        }

        if (interactionKeyText == null)
        {
            Debug.LogError("Interaction Key Text is not assigned in the inspector.");
            return;
        }

        if (interactableObjectText == null)
        {
            Debug.LogError("Interactable Object Text is not assigned in the inspector.");
            return;
        }

        if (playerInteraction == null)
        {
            Debug.LogError("Player Interaction is not assigned in the inspector.");
            return;
        }

        
        Hide();
    }
    private void Show()
    {
        containerInteractionKeyUI.SetActive(true);
    }

    private void Hide()
    {
        containerInteractionKeyUI.SetActive(false);
    }

    public void GetInteractableObjectText(InteractableInfo_TESTING interactableInfo)
    {

        InteractableSO_TESTING interactableObjectData = interactableInfo.interactableData;
        NPCDataSO_TESTING npcData = interactableInfo.npcData;

        if (interactableObjectData == null)
        {
            interactableObjectText.text = "Interact with ?????";
            Debug.LogWarning("Interactable Object Data is null.");
            return;
        }

        string interactableName = interactableObjectData.interactableName;

        if (string.IsNullOrEmpty(interactableName))
        {
            interactableName = interactableInfo.gameObject.name; // Fallback
        }

        //NPC Interactable
        if (interactableObjectData.interactableType == InteractableType.NPC && npcData != null)
        {
            if (!string.IsNullOrEmpty(npcData.npcName))
            {
                interactableName = npcData.npcName;
                
            }
            interactableObjectText.text = $"Talk with {interactableName}";
        }

        //Item Interactable
        else if (interactableObjectData.interactableType == InteractableType.Item)
        {
            interactableObjectText.text = $"Pick up {interactableName}";
        }

        //Door Interactable
        else if (interactableObjectData.interactableType == InteractableType.Door)
        {
            interactableObjectText.text = $"Open {interactableName}";
        }

        //Other Interactable
        else if (interactableObjectData.interactableType == InteractableType.Other)
        {
            interactableObjectText.text = $"Interact with {interactableName}";
        }
    }
    

    private void Update()
    {
        interactionKeyText.text = playerInteraction.interactionKey.ToString();


        if (playerInteraction.GetInteractableObject() != null)
        {
            GetInteractableObjectText(playerInteraction.GetInteractableObject());
            Show();
        }
        else
        {
            Hide();
        }
    }
}
