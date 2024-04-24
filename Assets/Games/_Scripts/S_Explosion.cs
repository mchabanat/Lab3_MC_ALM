using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    private float duration;
    void Start()
    {
        duration = explosion.main.duration;
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ennemy")
        {
            other.GetComponent<S_Enemy>().TakeDamage(50);
        }
        if (other.tag == "Player")
        {
            other.GetComponent<S_Player>().takeDamage(50);
        }
    }
}
