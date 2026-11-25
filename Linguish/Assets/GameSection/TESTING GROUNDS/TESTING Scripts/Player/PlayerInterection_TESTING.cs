using UnityEngine;

public class PlayerInterection_TESTING : MonoBehaviour
{

    /*
     * PLAYER INTERECTION
     * Use this code file to test player interaction and other related features. 
     * Play as much as u like.
     * ITS ATTACHED TO THE PLAYER PREFAB.
     * 
     * WE CAN GO WILDDDD HERE
     */

    [SerializeField] private float interactionRange = 2f;
    public KeyCode interactionKey = KeyCode.E;
    public KeyCode advanceDialogue = KeyCode.Space;

    [SerializeField] private DialogueUI_TESTING dialogueUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dialogueUI == null)
        {
            dialogueUI = FindAnyObjectByType<DialogueUI_TESTING>();
        }

        if (dialogueUI == null)
        {
            Debug.LogWarning("UI for Dialogue couldn't be found");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isDialogueOpen())
        {
            if (Input.GetKeyDown(advanceDialogue))
            {
                dialogueUI.CloseDialogue();
            }

            return;
        }

        if (Input.GetKeyDown(interactionKey))
        {
            InteractableInfo_TESTING interactable = GetInteractableObject();

            if (interactable == null)
            {
                return;
            }

            Debug.Log("Interacted with: " + interactable.name);

            /*
             * PLEASE READ ME FIRST
             * Here we can have the logic to grab an item,
             * open doors, etc.
             * Like interactable events kind of stuff.
             */

             //NPC
             NPCInterectable_TESTING interactableNPC = interactable.GetComponentInParent<NPCInterectable_TESTING>();

             if (interactableNPC != null)
            {
                interactableNPC.Interact();
            }

            else
            {
                Debug.Log("This interable doesn't have the NPC component.");
            }
        }
    }

    public bool isDialogueOpen()
    {
        if (dialogueUI != null)
        {
            if (dialogueUI.IsDialogueOpen() == true)
            {
                return true;
            }
        }

        return false;
    }

    public InteractableInfo_TESTING GetInteractableObject()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, interactionRange);

        InteractableInfo_TESTING closestInteractable = null;
        float closestDistance = interactionRange;

        foreach (Collider objects in nearbyObjects)
        {

            InteractableInfo_TESTING info = objects.GetComponentInParent<InteractableInfo_TESTING>();

            if (info != null)
            {
                if (objects.CompareTag("Interactable"))
                {
                    float distance = Vector3.Distance(transform.position, objects.ClosestPoint(transform.position));

                    if (distance <= closestDistance)
                    {
                        closestDistance = distance;
                        closestInteractable = info;
                    }
                }
            }
        }

        return closestInteractable;
    }
}
