using UnityEngine;

[CreateAssetMenu(fileName = "InteractableSO_TESTING", menuName = "Scriptable Objects/InteractableSO_TESTING")]
public class InteractableSO_TESTING : ScriptableObject
{
    public string interactableName;
    public InteractableType interactableType;
}

public enum InteractableType
{
    NPC,
    Item,
    Door,
    Other
}
