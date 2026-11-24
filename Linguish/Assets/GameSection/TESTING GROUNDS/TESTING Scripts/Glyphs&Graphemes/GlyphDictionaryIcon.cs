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

    public void setVisual(Grapheme grapheme, VisualStyle visualStyle = VisualStyle.IMAGE)
    {
        if(grapheme == null) return;
        graphemeImage.sprite = grapheme.graphemeImage;
        if(visualStyle == VisualStyle.IMAGE_AND_TEXT) graphemeName.text = grapheme.graphemeMeaning;
    }

    public void selectGrapheme()
    {
        transform.GetComponentInParent<GraphemeDictionary>()
            .displaySelectedGrapheme(transform.GetSiblingIndex());
    }

    public enum VisualStyle{
        IMAGE,
        IMAGE_AND_TEXT
    }
}
