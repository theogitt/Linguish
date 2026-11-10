using UnityEngine;
using Unity.Cinemachine;

public class CineMachineCameraPrefabScript : MonoBehaviour
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

        if (player == null && CineCamera == null)
        {
            Debug.LogError("CineMachineCameraPrefabScript: Prob Forgot to add the 'Player' tag to your player, and/or the CinemachineCamera component is missing.");
            return;
        }

        else
        {
            CineCamera.Follow = player.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
