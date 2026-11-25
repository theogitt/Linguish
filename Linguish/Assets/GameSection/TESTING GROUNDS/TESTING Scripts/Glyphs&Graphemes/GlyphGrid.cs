using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class GlyphGrid : MonoBehaviour
{
    private Dictionary<RectTransform, int> usedTransforms = new Dictionary<RectTransform, int>();
    public Action<bool> phraseValidated;
    [SerializeField] Word correctPhrase;
    private void Start() {
        phraseValidated += print;
    }
    private void OnEnable()
    {
        usedTransforms.Clear();
    }
    public void snapToGrid(RectTransform rectTransform)
    {
        foreach(RectTransform rect in GetComponentsInChildren<RectTransform>())
        {
            if(rect.transform == this.transform) continue;
            if(isOverlapping(rectTransform, rect))
            {
                rectTransform.position = rect.position;
                assignGrapheme(rect.GetSiblingIndex(), rectTransform);
                return;
            }
        }
        if(usedTransforms.ContainsKey(rectTransform)) usedTransforms.Remove(rectTransform);
    }

    public void assignGrapheme(int index, RectTransform rectTransform)
    {
        if(!usedTransforms.ContainsKey(rectTransform)) usedTransforms.Add(rectTransform, index);
        else usedTransforms[rectTransform] = index;
    }

    public void validatePhrase()
    {
        Word[] graphs = new Word[6];
        //Makes a list of graphemes from the attached parts
        foreach(KeyValuePair<RectTransform, int> keyValuePair in usedTransforms)
        {
            graphs[keyValuePair.Value] = keyValuePair.Key.GetComponent<GraphemeDragger>().getGrapheme();
        }
        
        //If a part of the correct phrase is not correct then return false
        for(int i = 0; i < 6; i++)
        {
            if(correctPhrase.composition[i] != graphs[i])
            {
                phraseValidated.Invoke(false);
                return;
            } 
        }
        //Tell it that its true
        phraseValidated.Invoke(true);
    }

    private void print(bool b)
    {
        if(b) Debug.Log("Validated");
        else Debug.Log("Unvalidated");
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
