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
        Debug.Log("Explosion hit: " + other.name);
    }
}
