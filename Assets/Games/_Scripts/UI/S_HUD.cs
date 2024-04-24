using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class S_HUD : MonoBehaviour
{

    Label ammoLabel;
    Label healthLabel;
    Label timeLabel;

    VisualElement menu;
    Button resumeButton;
    Button backMenuButton;

    VisualElement gameOver;
    Button retryButton;
    Button backToMenu;

    bool isPaused = false;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ammoLabel = root.Q<Label>("Ammo");
        healthLabel = root.Q<Label>("Health");
        timeLabel = root.Q<Label>("Time");
        menu = root.Q<VisualElement>("pause");
        resumeButton = root.Q<Button>("buttonResume");
        backMenuButton = root.Q<Button>("buttonMenu");

        gameOver = root.Q<VisualElement>("mort");
        retryButton = root.Q<Button>("buttonRetry");
        backToMenu = root.Q<Button>("buttonMenuGO");

        resumeButton.clicked += Resume;
        backMenuButton.clicked += BackMenu;

        retryButton.clicked += Retry;
        backToMenu.clicked += BackMenu;


        menu.style.display = DisplayStyle.None;
        gameOver.style.display = DisplayStyle.None;


    }

    public void UpdateAmmo(int ammo)
    {
        ammoLabel.text = "Ammo: " + ammo;
    }

    public void UpdateHealth(int health)
    {
        healthLabel.text = health + "%";
    }

    public void Pause()
    {
        menu.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        isPaused = true;
    }

    public void Update()
    {
        timeLabel.text = "Time: " + (int)Time.time;

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }

        else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }

    }

    public void Resume()
    {
        menu.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        isPaused = false;
    }

    public void BackMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        gameOver.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }
    public void Retry()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
