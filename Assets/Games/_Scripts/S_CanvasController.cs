using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_CanvasController : MonoBehaviour
{
    [SerializeField] private TMP_Text _lifeText;
    [SerializeField] private TMP_Text _forwardBosstText;

    private void Start()
    {
        TextColorGreen(_forwardBosstText);
    }

    public void TextColorRed(TMP_Text txt)
    {
        txt.color = Color.red;
    }
    public void TextColorGreen(TMP_Text txt)
    {
        txt.color = Color.green;
    } 

    public void UpdateForwardBoostText(bool isReady)
    {
        if (isReady)
        {
            _forwardBosstText.text = "Boost Disponible";
            TextColorGreen(_forwardBosstText);
        } else
        {
            _forwardBosstText.text = "Boost Temporairement Epuisé";
            TextColorRed(_forwardBosstText);
        }
    }

    public void UpdateLifeText(int lifePoints)
    {
        _lifeText.text = "Points de vie restants : " + lifePoints;
    }
}
