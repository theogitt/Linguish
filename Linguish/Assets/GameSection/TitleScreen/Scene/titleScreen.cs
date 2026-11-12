using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class titleScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button StartButton;
    public Button SettingsButton;
    public Button CreditsButton;
    public Button QuitButton;
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;

    void Start()
    {
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Button startButton  = StartButton.GetComponent<Button>();
        startButton.onClick.AddListener(onStart);

        Button settingsButton = SettingsButton.GetComponent<Button>();
        settingsButton.onClick.AddListener(onSettings);

        Button creditsButton = SettingsButton.GetComponent<Button>();
        creditsButton.onClick.AddListener(onCredits);

        Button quitButton = QuitButton.GetComponent<Button>();
        quitButton.onClick.AddListener(onQuit);

    }

    public void onStart()
    {
        SceneManager.LoadScene(1);
    }

    public void onSettings()
    {
        SettingsPanel.SetActive(true);
    }

    public void onCredits()
    {
        CreditsPanel.SetActive(true);
    }

    public void onQuit()
    {
        Application.Quit();
    }
}
