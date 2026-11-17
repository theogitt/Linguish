using UnityEngine;

[CreateAssetMenu(fileName = "NPCData_TESTING", menuName = "Scriptable Objects/NPCData_TESTING")]
public class NPCData_TESTING : ScriptableObject
{
    public string npcName;
    public string npcDescription;
    public string npcRole;
    public string npcSociety;
    public Sprite npcImage;

    [TextArea(3, 10)]
    public string npcDialogue;
}
