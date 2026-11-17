using UnityEngine;

[CreateAssetMenu(fileName = "Phrase", menuName = "Linguish/Phrase")]
public class Phrase : ScriptableObject
{
    [Tooltip("Element 0 is [0,0] (top left) and element 5 is [2,3] (top right)")]public Grapheme[] phrase = new Grapheme[6];
}
