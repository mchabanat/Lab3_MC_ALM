using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MoreGravityZone : MonoBehaviour
{
    private float _initialMass;
    [SerializeField] private float _newMass = 100;


    private void OnTriggerEnter(Collider other)
    {
        _initialMass = other.gameObject.GetComponent<Rigidbody>().mass;
        other.gameObject.GetComponent<Rigidbody>().mass = _newMass;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().mass = _initialMass;
    }
}
