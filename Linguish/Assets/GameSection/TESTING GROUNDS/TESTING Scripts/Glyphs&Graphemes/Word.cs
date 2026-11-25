using Unity.GraphToolkit.Editor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Word", menuName = "Linguish/Grapheme")]
public class Word : ScriptableObject
{
    public Sprite wordImage;
    public string wordMeaning;
    [Tooltip("Element 0 is [0,0] (top left) and element 5 is [2,3] (top right)")]public Grapheme[] composition = new Grapheme[6];
    [Tooltip("Element 0 is [0,0] (top left) and element 5 is [2,3] (top right)")]public int[] rotations = new int[6];
}
