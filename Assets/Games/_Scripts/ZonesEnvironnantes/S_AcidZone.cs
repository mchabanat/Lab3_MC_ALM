using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_AcidZone : MonoBehaviour
{
    private bool _playerDamageable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerDamageable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Le joueur prend des dégâts toutes les secondes
            if (_playerDamageable)
            {
                TakeDamage(other.gameObject);
                _playerDamageable = false;
                Invoke("ResetDamageable", 1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerDamageable = true;
        }
    }

    private void TakeDamage(GameObject player)
    {
        // Le joueur prend des dégâts
        player.GetComponent<S_Player>().takeDamage(5);
    }

    private void ResetDamageable()
    {
        _playerDamageable = true;
    }
}
