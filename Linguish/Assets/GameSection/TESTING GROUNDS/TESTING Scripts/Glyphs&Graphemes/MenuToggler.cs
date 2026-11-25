using UnityEngine;

public class MenuToggler : MonoBehaviour
{
    [SerializeField] GameObject menuToToggle;

    private void Start()
    {
        menuToToggle.SetActive(false);
    }

    public void toggleVisibility() => menuToToggle.SetActive(!menuToToggle.activeSelf);
}
