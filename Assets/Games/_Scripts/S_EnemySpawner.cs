using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _playerToHunt;

    private GameObject newEnemy;

    private bool _spawnReady = true;

    void Update()
    {
        if (_spawnReady)
        {
            _spawnReady = false;
            Invoke("ResetSpawnReady", 5f);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.GetComponent<S_Enemy>().SetPlayerTransform(_playerToHunt);
        Invoke("KillIA", 3f);
    }

    private void ResetSpawnReady()
    {
        _spawnReady = true;
    }

    private void KillIA()
    {
        newEnemy.GetComponent<S_Enemy>().Die();
    }
}
