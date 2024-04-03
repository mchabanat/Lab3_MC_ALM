using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class S_Menu : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("buttonPlay");
        Button settingsButton = root.Q<Button>("buttonSettings");
        Button quitButton = root.Q<Button>("buttonQuit");

        startButton.clicked += () => play();
        settingsButton.clicked += () => settings();
        quitButton.clicked += () => quit();

    }

    private void play()
    {
        Debug.Log("Play");
    }
    private void settings()
    {
        Debug.Log("Settings");
    }
    private void quit()
    {
        Debug.Log("Quit");
    }
}
