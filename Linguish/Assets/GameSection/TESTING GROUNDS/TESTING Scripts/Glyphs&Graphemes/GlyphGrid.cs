using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class GlyphGrid : MonoBehaviour
{
    [SerializeField] private Grapheme[] graphemeGrid = new Grapheme[6];
    private Dictionary<RectTransform, int> usedTransforms = new Dictionary<RectTransform, int>();
    [SerializeField] Phrase correctPhrase;
    public void snapToGrid(RectTransform rectTransform)
    {
        foreach(RectTransform rect in GetComponentsInChildren<RectTransform>())
        {
            if(rect.transform == this.transform) continue;
            if(isOverlapping(rectTransform, rect))
            {
                rectTransform.position = rect.position;
                assignGrapheme(rect.GetSiblingIndex(), rectTransform.GetComponent<GraphemeDragger>().getGrapheme());
                if(!usedTransforms.ContainsKey(rectTransform)) usedTransforms.Add(rectTransform, rect.GetSiblingIndex());
                else usedTransforms[rectTransform] = rect.GetSiblingIndex();
                foreach(KeyValuePair<RectTransform, int> keyValuePair in usedTransforms)
                {
                    Debug.Log(keyValuePair.Value);
                }
                return;
            }
        }
        if(usedTransforms.ContainsKey(rectTransform)) usedTransforms.Remove(rectTransform);
    }

    public void assignGrapheme(int index, Grapheme grapheme)
    {
        graphemeGrid[index] = grapheme;
    }

    public void validatePhrase()
    {
        for(int i = 0; i < graphemeGrid.Length; i++)
        {
            if(graphemeGrid[i] != correctPhrase.phrase[i])
            {
                Debug.Log("NO"); 
                return;
            }
        }
        Debug.Log("YES");
    }

    private bool isOverlapping(RectTransform obj1, RectTransform obj2)
    {
        if(obj1 == false || obj2 == false) return false;

        Vector3[] cornersA = new Vector3[4];
        Vector3[] cornersB = new Vector3[4];

        obj1.GetWorldCorners(cornersA);
        obj2.GetWorldCorners(cornersB);

        Rect rect1 = new Rect(cornersA[0], cornersA[2] - cornersA[0]);
        Rect rect2 = new Rect(cornersB[0], cornersB[2] - cornersB[0]);

        return rect1.Overlaps(rect2);
    }
}
