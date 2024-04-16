using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class S_Menu : MonoBehaviour
{
    VisualElement menuContainer;
    VisualElement settingsContainer;

    DropdownField dropDownScreenMode;
    DropdownField dropDownScreenResolution;
    Slider volumeSlider;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("buttonPlay");
        Button settingsButton = root.Q<Button>("buttonSettings");
        Button quitButton = root.Q<Button>("buttonQuit");

        Button menuButton = root.Q<Button>("menuButton");
        Button applyButton = root.Q<Button>("applyButton");

        dropDownScreenMode = root.Q<DropdownField>("dropDownScreenMode");
        dropDownScreenResolution = root.Q<DropdownField>("dropDownScreenSize");
        volumeSlider = root.Q<Slider>("volumeSlider");

        menuContainer = root.Q<VisualElement>("ContainerMenu");
        settingsContainer = root.Q<VisualElement>("ContainerSettings");

        menuContainer.style.display = DisplayStyle.Flex;
        settingsContainer.style.display = DisplayStyle.None;

        dropDownScreenResolution.choices = new List<string> { "1920x1080", "1280x720", "800x600" };
        dropDownScreenMode.choices = new List<string> { "Windowed", "Fullscreen" };

        if (Screen.fullScreen)
        {
            dropDownScreenMode.value = "Fullscreen";
        }
        else
        {
            dropDownScreenMode.value = "Windowed";
        }

        if(Screen.width == 1920 && Screen.height == 1080)
        {
            dropDownScreenResolution.value = "1920x1080";
        }
        else if (Screen.width == 1280 && Screen.height == 720)
        {
            dropDownScreenResolution.value = "1280x720";
        }
        else
        {
            dropDownScreenResolution.value = "800x600";
        }


        startButton.clicked += () => play();
        settingsButton.clicked += () => settings();
        quitButton.clicked += () => quit();

        menuButton.clicked += () => backToMenu();
        applyButton.clicked += () => applySettings();
    }

    private void play()
    {
        SceneManager.LoadScene("LVL_MainMap");
    }
    private void settings()
    {
        menuContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.Flex;
    }
    private void quit()
    {
        Application.Quit();
    }

    private void backToMenu()
    {
        menuContainer.style.display = DisplayStyle.Flex;
        settingsContainer.style.display = DisplayStyle.None;
    }

    private void applySettings()
    {
        if (dropDownScreenMode.value == "Fullscreen")
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        Screen.SetResolution(int.Parse(dropDownScreenResolution.value.Split('x')[0]), int.Parse(dropDownScreenResolution.value.Split('x')[1]), Screen.fullScreen);
    }
}
