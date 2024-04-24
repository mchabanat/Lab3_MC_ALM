using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Enemy : MonoBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private int _lifePoints = 50;

    [SerializeField] private GameObject _deathVFX;

    [SerializeField] private GameObject _bonusAmmo;

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
        // Ajout d'une explosion à la mort de l'ennemi
        GameObject bloodExplosion = Instantiate(_deathVFX, transform.position, Quaternion.identity);
        Destroy(bloodExplosion, 2f);

        //Ajout d'un bonus de munitions à la mort de l'ennemi
        GameObject ammoBonus = Instantiate(_bonusAmmo, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<S_Player>().takeDamage(10);
        }
    }
}
