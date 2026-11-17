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
    [SerializeField] public KeyCode interactionKey = KeyCode.E;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);  

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Interactable"))
                {
                    /*
                        * PLEASE READ ME FIRST
                        * Here we can have the logic to grab an item, open doors, etc.
                        * Like interactable events kind of stuff.
                        */

                    Debug.Log("Interacted with: " + hitCollider.name); //Leave this here for now, we can remove it later. It is just to test if the interaction is working.

                    NPCInterectable_TESTING interactableNPC = hitCollider.GetComponent<NPCInterectable_TESTING>();

                    if (interactableNPC != null)
                    {
                        interactableNPC.Interact();
                    }

                }
            }
        }  
    }

    public InteractableInfo_TESTING GetInteractableObject()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, interactionRange);

        foreach (Collider objects in nearbyObjects)
        {
            if (objects.CompareTag("Interactable"))
            {
                InteractableInfo_TESTING info = objects.GetComponentInParent<InteractableInfo_TESTING>();
                if (info != null)
                {
                    return info;
                }
            }
        }

        return null; // No interactable NPC found
    }
}
