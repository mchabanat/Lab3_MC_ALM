using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Projectile : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private float force = 1000f;
    [SerializeField] private bool isExplosive = false;
    [SerializeField] private bool isBouncy = false;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float numberOfBounce = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        impulse();
    }

    void impulse()
    {

        if (isExplosive)
        {
            trail.startColor = Color.red;
            trail.endColor = Color.yellow;
            force = force/2;
        }

        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        Destroy(gameObject, duration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isExplosive)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        if (isBouncy)
        {
            if (numberOfBounce > 0)
            {
                numberOfBounce--;
                Debug.Log("Bounce: " + numberOfBounce);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
