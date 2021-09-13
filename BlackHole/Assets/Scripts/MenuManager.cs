using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Text volumeText;

    AudioSource source;

    private void Start()
    {
        source = AudioManager.Instance.GetComponent<AudioSource>();
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
