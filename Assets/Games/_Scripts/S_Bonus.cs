using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bonus : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 10.0f;

    void Start()
    {

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
}
