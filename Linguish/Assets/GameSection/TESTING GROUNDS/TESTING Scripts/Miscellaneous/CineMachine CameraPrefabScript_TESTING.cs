using UnityEngine;
using Unity.Cinemachine;

public class CineMachineCameraPrefabScript_TESTING : MonoBehaviour
{

    /*
     * CINEMACHINE CAMERA PREFAB SCRIPT
     * This script is used to set the Cinemachine camera to follow the player.
     * IT IS ATTACHED TO THE CINEMACHINE CAMERA PREFAB.
     * 
     * NOTE: This script is not required for the Cinemachine camera to work, but since its a preab and we may forget to add the player to the tracking so just made this to automate it.
     */

    //DEBUG
    public CinemachineCamera CineCamera;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CineCamera = GetComponent<CinemachineCamera>();

        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("CineMachineCameraPrefabScript: Prob forgot to add the Player gameobject.");
            return;
        }

        if (CineCamera == null)
        {
            Debug.LogError("CineMachineCameraPrefabScript: Prob forgot to add the CinemachineCamera component is missing.");
            return;
        }


        CineCamera.Follow = player.transform;


    }
}
