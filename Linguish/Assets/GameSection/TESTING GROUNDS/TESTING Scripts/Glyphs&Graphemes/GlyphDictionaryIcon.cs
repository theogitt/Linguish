using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlyphDictionaryIcon : MonoBehaviour
{
    private const string LOCKED_GRAPHEME_TEXT = "(???)";
    private Image graphemeImage;
    private TextMeshProUGUI graphemeName;

    public void Awake()
    {
        graphemeImage = GetComponent<Image>();
        graphemeName = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void setVisual(Grapheme grapheme)
    {
        if(grapheme == null) return;
        graphemeImage.sprite = grapheme.graphemeImage;
        graphemeName.text = grapheme.graphemeMeaning;
    }
}
