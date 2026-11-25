using System.ComponentModel;
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
    [SerializeField] private Vector2 offset = new Vector2(80f, 20f); // Use this to move around the helper, we can just leave it here after just in case we want to move it again


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

            playerInteraction = FindAnyObjectByType<PlayerInterection_TESTING>();
        }

        if (mainCamera == null)
        {
            Debug.Log("MAIN CAMERA:Main Camera not found, attempting to find it");
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


    private void Update() //Testing thigns out change to "LateUpdate()" if anything seems delayed
    {

        if (playerInteraction == null)
        {
            Debug.LogError("Player Interaction is not assigned in the inspector. Attempting to find it in the scene...");
            playerInteraction = FindAnyObjectByType<PlayerInterection_TESTING>();

            if (playerInteraction == null)
            {
                Debug.LogError("Player Interaction could not be found in the scene.");
                Hide();
                return;
            }
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Hide();
                return;
            }
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



        InteractableInfo_TESTING interactable = playerInteraction.GetInteractableObject(); //What can the player interact with?

        if (interactable == null)
        {
            Hide();
            return;
        }

        Vector3 objectPosition = interactable.transform.position;

        objectPosition.y = objectPosition.y + 1.5f; //Leave this for now, it is to move the UI helper above the model (Key, Door, NPC, etc)


        if (interactable.interactionHelper != null)
        {
            objectPosition = interactable.interactionHelper.position;
        }

        Vector3 screenPosition = mainCamera.WorldToScreenPoint(objectPosition);


        //We checking if the player has gone behind the UI helper or not
        if (screenPosition.z <= 0)
        {
            Hide();
            return;
        }

        screenPosition.x = screenPosition.x + offset.x;
        screenPosition.y = screenPosition.y + offset.y;

        containerInteractionKeyUI.transform.position = screenPosition;

        interactionKeyText.text = playerInteraction.interactionKey.ToString();


        if (playerInteraction.GetInteractableObject() != null)
        {
            GetInteractableObjectText(interactable);
            Show();
        }
        else
        {
            Hide();
        }
    }
}
