using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Player : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private int _lifePoints = 200;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void UpdateCanvasLife()
    {
        _canvas.GetComponent<S_CanvasController>().UpdateLifeText(_lifePoints);
    }

    public void AddLifePoints(int lifePoints)
    {
        _lifePoints += lifePoints;
        UpdateCanvasLife();
    }

    public void TakeDamage(int damage)
    {
        _lifePoints -= damage;
        UpdateCanvasLife();

        if (_lifePoints <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Player died");
    }
}
