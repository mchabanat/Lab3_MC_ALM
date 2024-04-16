using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class S_HUD : MonoBehaviour
{

    Label ammoLabel;
    Label healthLabel;
    Label timeLabel;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ammoLabel = root.Q<Label>("Ammo");
        healthLabel = root.Q<Label>("Health");
        timeLabel = root.Q<Label>("Time");
    }

    public void UpdateAmmo(int ammo)
    {
        ammoLabel.text = "Ammo: " + ammo;
    }

    public void UpdateHealth(int health)
    {
        healthLabel.text = health + "%";
    }

    public void Update()
    {
        timeLabel.text = "Time: " + (int)Time.time;
    }
}
