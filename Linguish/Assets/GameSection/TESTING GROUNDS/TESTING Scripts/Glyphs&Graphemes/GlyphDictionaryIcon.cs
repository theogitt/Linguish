using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GlyphDictionaryIcon : MonoBehaviour
{
    private const string LOCKED_GRAPHEME_TEXT = "(???)";
    private static RectTransform selectedGrapheme = null;
    private Image graphemeImage;
    private TextMeshProUGUI graphemeName;
    private int angleCounts = 0;
    [SerializeField] private int currentAngle = 0;

    public int CurrentAngle
    {
        get {return this.currentAngle;}
        set {this.currentAngle = value;}
    }

    public void Awake()
    {
        graphemeImage = GetComponent<Image>();
        graphemeName = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1) && selectedGrapheme == (RectTransform)this.transform && RectTransformUtility.RectangleContainsScreenPoint((RectTransform)this.transform,Input.mousePosition))
        {
            Debug.Log("HALLO");
            this.currentAngle += 1;
            selectGrapheme();
        }
    }

    public void setVisual(Word grapheme, VisualStyle visualStyle = VisualStyle.IMAGE)
    {
        if(grapheme == null) return;
        this.currentAngle = 0;
        //graphemeImage.sprite = grapheme.graphemeImage;
        //if(visualStyle == VisualStyle.IMAGE_AND_TEXT) graphemeName.text = grapheme.graphemeMeanings[currentAngle];
    }

    public void selectGrapheme()
    {
        transform.GetComponentInParent<GraphemeDictionary>()
            .displaySelectedGrapheme(transform.GetSiblingIndex(), currentAngle);
        selectedGrapheme = (RectTransform)this.transform;
    }

    public int getAngle()
    {
        return this.currentAngle;
    }

    public enum VisualStyle{
        IMAGE,
        IMAGE_AND_TEXT
    }
}
