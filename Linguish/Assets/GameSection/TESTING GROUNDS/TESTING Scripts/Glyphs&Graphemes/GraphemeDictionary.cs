using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class GraphemeDictionary : MonoBehaviour
{
    [SerializeField] private List<Grapheme> lockedGraphemes = new List<Grapheme>();
    [SerializeField] private List<Grapheme> unlockedGraphemes = new List<Grapheme>();

    [SerializeField] private Sprite lockedGraphemeSprite;
    [SerializeField] private string lockedGraphemeName = "???";

    public List<Grapheme> LockedGraphemes
    {
        get {return this.lockedGraphemes;}
    }
    public List<Grapheme> UnlockedGraphemes
    {
        get {return this.unlockedGraphemes;}
    }

    public void unlockGrapheme(Grapheme grapheme)
    {
        if(!lockedGraphemes.Contains(grapheme)) return;
        else
        {
            lockedGraphemes.Remove(grapheme);
            unlockedGraphemes.Add(grapheme);
        }
    }

    public override string ToString()
    {
        StringBuilder returnString = new StringBuilder();

        foreach(Grapheme g in unlockedGraphemes)
            returnString.Append(g.graphemeMeaning + "\n");
        foreach(Grapheme g in lockedGraphemes)
            returnString.Append(g.graphemeMeaning + " (???)\n");
        
        return returnString.ToString();
    }

    private void Start() {
        Debug.Log(this.ToString());
        FindAnyObjectByType<GlyphDictionaryIcon>().setVisual(unlockedGraphemes[0]);
    }
}
