using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Text volumeText;

    AudioSource source;

    public GameObject quitButton;

    private void Start()
    {
        source = AudioManager.Instance.GetComponent<AudioSource>();

#if UNITY_STANDALONE || UNITY_EDITOR
        quitButton.SetActive(true);
#else
        quitButton.SetActive(false);
#endif
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartLocalGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (source == null || volumeSlider == null || volumeText == null) return;

        source.volume = volumeSlider.value / volumeSlider.maxValue;
        volumeText.text = $"{Mathf.RoundToInt(volumeSlider.value)}";
    }
}
