using TMPro;
using UnityEngine;

public class TestText : MonoBehaviour
{
    private void Start()
    {
        FindAnyObjectByType<GlyphGrid>().phraseValidated += displayValidationMessage;
    }
    public void displayValidationMessage(bool successfullyValidated)
    {
        if(successfullyValidated) GetComponent<TextMeshProUGUI>().text = "CORRECT";
        else GetComponent<TextMeshProUGUI>().text = "Wrong. Try again!";
    }
}
