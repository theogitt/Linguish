using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

public class GraphemeDragger : MonoBehaviour
{
    private bool isDragging = false;
    private RectTransform rectTransform;
    private static RectTransform currentlyHeldTransform = null;
    [SerializeField] private Grapheme grapheme;
    private void Start() {
        rectTransform = GetComponent<RectTransform>();
    }
    public void toggleDrag()
    {
        if(currentlyHeldTransform != null)
        {
            if(currentlyHeldTransform != rectTransform) return;
        }
        this.isDragging = !this.isDragging;
        if (!this.isDragging)
        {
            FindAnyObjectByType<GlyphGrid>().snapToGrid(rectTransform);
            currentlyHeldTransform = null;
        }
        else
        {
            currentlyHeldTransform = rectTransform;
        }
    }
    private void Update() {
        if(isDragging) rectTransform.position = Input.mousePosition;
    }

    public Grapheme getGrapheme()
    {
        return this.grapheme;
    }
}
