using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bonus : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 10.0f;

    [SerializeField] private int ammo = 10;

    [SerializeField] private float timeBeforeDestroy = 10f;

    void Start()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        rotateBonus();
    }

    private void rotateBonus()
    {
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<S_Player>().AddAmmo(ammo);
            Destroy(gameObject);
        }
    }
}
