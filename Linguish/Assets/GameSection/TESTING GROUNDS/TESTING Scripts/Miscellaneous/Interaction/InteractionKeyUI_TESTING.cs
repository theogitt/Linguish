using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionKeyUI_TESTING : MonoBehaviour
{
    [SerializeField] private GameObject containerInteractionKeyUI;
    [SerializeField] private PlayerInterection_TESTING playerInteraction;

    [SerializeField] private TextMeshProUGUI interactionKeyText;
    [SerializeField] private TextMeshProUGUI interactableObjectText;


    [SerializeField] private Camera mainCamera;
    // [SerializeField] private Vector2 offset = new System.Numerics.Vector2(80f, 20f); // Use this to move around the helper, we can just leave it here after just in case we want to move it again


    private void Start()
    {

        if (containerInteractionKeyUI == null)
        {
            Debug.LogError("Container Interaction Key UI is not assigned in the inspector.");
            enabled = false;
            return;
        }

        if (interactionKeyText == null)
        {
            Debug.LogError("Interaction Key Text is not assigned in the inspector.");
            enabled = false;
            return;
        }

        if (interactableObjectText == null)
        {
            Debug.LogError("Interactable Object Text is not assigned in the inspector.");
            enabled = false;
            return;
        }

        if (playerInteraction == null)
        {
            Debug.LogError("PLAYER INTERACTION: Player Interaction is not assigned in the inspector. Attempting to find it in the scene...");

            playerInteraction = FindFirstObjectByType<PlayerInterection_TESTING>();
        }

        if (mainCamera == null)
        {
            Debug.Log ("MAIN CAMERA:Main Camera not found, attempting to find it");
            mainCamera = Camera.main;
        }

        if (mainCamera == null)
        {
            Debug.Log("MAIN CAMERA: G u sure u seeing the game, cause we cant find th emain camera");
            enabled = false;
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
            interactableObjectText.text = "Interact"; // Yo Ethan please clarify if I should keep the "?????", I had it there just in case we had lik a mysterious item.
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

        if (playerInteraction == null)
        {
            Debug.LogError("Player Interaction is not assigned in the inspector. Attempting to find it in the scene...");
            playerInteraction = FindFirstObjectByType<PlayerInterection_TESTING>();

            if (playerInteraction == null)
            {
                Debug.LogError("Player Interaction could not be found in the scene.");
                Hide();
                return;
            }
        }

        if (mainCamera == null)
        {
            Hide();
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
