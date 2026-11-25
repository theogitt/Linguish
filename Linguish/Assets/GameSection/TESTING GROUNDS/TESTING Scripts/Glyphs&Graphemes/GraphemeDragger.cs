using System;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

public class GraphemeDragger : MonoBehaviour
{
    private bool isDragging = false;
    private RectTransform rectTransform;
    private static RectTransform currentlyHeldTransform = null;
    private Vector3 resetPosition;
    [SerializeField] private Word grapheme;
    [SerializeField] private int rotation;
    [SerializeField] GameObject gameObjec;
    
    private void Awake()
    {
        resetPosition = transform.position;
    }
    private void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable() {
        transform.position = resetPosition;
    }
    public void toggleDrag()
    {
        if(currentlyHeldTransform != null)
        {
            if(currentlyHeldTransform != rectTransform) return;
        }
        createDuplicate();
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

    public void createDuplicate()
    {
        Instantiate(gameObjec, resetPosition, quaternion.identity, transform.parent);
    }

    private void Update() {
        if(isDragging) rectTransform.position = Input.mousePosition;
    }

    public Word getGrapheme()
    {
        return this.grapheme;
    }
}
