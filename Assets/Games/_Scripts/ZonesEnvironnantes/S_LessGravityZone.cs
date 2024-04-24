using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LessGravityZone : MonoBehaviour
{
    private float _initialMass;
    private void OnTriggerEnter(Collider other)
    {
        _initialMass = other.gameObject.GetComponent<Rigidbody>().mass;
        other.gameObject.GetComponent<Rigidbody>().mass = _initialMass / 2;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().mass = _initialMass;
    }
}
