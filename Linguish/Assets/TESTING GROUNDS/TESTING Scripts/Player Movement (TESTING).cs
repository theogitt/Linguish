using UnityEngine;


/*
 * PLAYER MOVEMENT 
 * Use this code file to test player movement and other related features. 
 * Play as much as u like.
 * ITS ATTACHED TO THE PLAYER PREFAB.
 * 
 * WE CAN GO WILDDDD HERE
 */

public class PlayerMovement : MonoBehaviour
{

    public CharacterController pController;

    [SerializeField] public float spd = 12f;

    [SerializeField] public float SmoothTurn = 0.1f;
    [SerializeField] public float turnSmoothVelocity = 0.1f;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0, z).normalized;

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg; //Rotating the player to where they are moving towards
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, SmoothTurn); //Smoothly rotating the player to the target angle, instead of snapping to it
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            pController.Move(move * spd * Time.deltaTime);
        }
    }
}
