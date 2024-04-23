using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_AcidZone : MonoBehaviour
{
    private bool _isDamaging = false;
    private bool _playerDamageable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isDamaging = true;
            _playerDamageable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Le joueur prend des d�g�ts toutes les secondes
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
            Invoke("ResetIsDamaging", 5f);
            _playerDamageable = true;

            // Prend des d�gats toutes les secondes pendant 5 secondes apr�s avoir quitt� la zone
            if (_playerDamageable)
            {
                TakeDamage(other.gameObject);
                _playerDamageable = false;
                Invoke("ResetDamageable", 1f);
            }
        }
    }

    private void TakeDamage(GameObject player)
    {
        // Le joueur prend des d�g�ts
        player.GetComponent<S_Player>().takeDamage(5);
    }

    private void ResetDamageable()
    {
        _playerDamageable = true;
    }

    private void ResetIsDamaging()
    {
        _isDamaging = false;
    }
}
