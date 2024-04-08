using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_CanvasController : MonoBehaviour
{
    [SerializeField] private TMP_Text _lifeText;

    public void UpdateLifeText(int lifePoints)
    {
        _lifeText.text = "Points de vie restants : " + lifePoints;
    }
}
