using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class S_HUD : MonoBehaviour
{

    Label ammoLabel;
    Label healthLabel;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ammoLabel = root.Q<Label>("Ammo");
        healthLabel = root.Q<Label>("Health");
    }

    public void UpdateAmmo(int ammo)
    {
        ammoLabel.text = "Ammo: " + ammo;
    }

    public void UpdateHealth(int health)
    {
        healthLabel.text = health + "%";
    }
}
