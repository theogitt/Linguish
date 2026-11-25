using UnityEngine;

[CreateAssetMenu(fileName = "NPCData_TESTING", menuName = "Scriptable Objects/NPCData_TESTING")]
public class NPCDataSO_TESTING : ScriptableObject
{
    public string npcName;
    public string npcDescription;
    public string npcRole;
    public string npcSociety;
    public Sprite npcImage;

    [TextArea(3, 10)]
    public string npcDialogue;

    [TextArea(2, 5)]
    public string[] npcSpeechBubble;
}
