using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Enemy : MonoBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private int _lifePoints = 50;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _navMeshAgent.SetDestination(_playerTransform.position);
    }

    public void SetPlayerTransform(GameObject player)
    {
        _playerTransform = player.transform;
    }

    public void TakeDamage(int damage)
    {
        _lifePoints -= damage;

        if (_lifePoints <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);

        // Ajout d'une explosion à la mort de l'ennemi
    }
}
