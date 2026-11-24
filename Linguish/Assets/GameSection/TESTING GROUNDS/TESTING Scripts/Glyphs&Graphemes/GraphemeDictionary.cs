using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GraphemeDictionary : MonoBehaviour
{
    [SerializeField] private int unlockableGraphemeCount = 16;
    [SerializeField] private List<Grapheme> unlockedGraphemes = new List<Grapheme>();
    [SerializeField] private Grapheme nullGrapheme;

    [Header("Selected Grapheme Display")]
    [SerializeField] private Image displayImage;
    [SerializeField] private TextMeshProUGUI displayText;
    public List<Grapheme> UnlockedGraphemes
    {
        get {return this.unlockedGraphemes;}
    }

    public void unlockGrapheme(Grapheme grapheme)
    {
        if(unlockedGraphemes.Contains(grapheme)) return;
        else unlockedGraphemes.Add(grapheme);
        
    }

    public override string ToString()
    {
        StringBuilder returnString = new StringBuilder();

        foreach(Grapheme g in unlockedGraphemes)
            returnString.Append(g.graphemeMeaning + "\n");
        
        return returnString.ToString();
    }

    private void Start()
    {
        GlyphDictionaryIcon[] icons = GetComponentsInChildren<GlyphDictionaryIcon>();
        for(int i = 0; i < unlockedGraphemes.Count; i++)
        {
            icons[i].setVisual(unlockedGraphemes[i]);
        }
        for(int i = unlockedGraphemes.Count; i < icons.Length; i++)
        {
            icons[i].setVisual(nullGrapheme);
        }
    }

    public void displaySelectedGrapheme(int dictionarySelection)
    {
        this.displayImage.sprite = unlockedGraphemes[dictionarySelection].graphemeImage;
        this.displayText.text = unlockedGraphemes[dictionarySelection].graphemeMeaning;
    }
}
