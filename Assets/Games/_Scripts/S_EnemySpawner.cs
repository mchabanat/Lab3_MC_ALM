using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _playerToHunt;


    [SerializeField] private float _baseSpawnRate = 5f;
    private float _spawnRate;
    private bool _spawnReady = true;

    void Start()
    {
        _spawnRate = _baseSpawnRate;
    }

    void Update()
    {
        if (_spawnReady)
        {
            _spawnReady = false;
            Invoke("ResetSpawnReady", _spawnRate);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.GetComponent<S_Enemy>().SetPlayerTransform(_playerToHunt);
    }

    private void ResetSpawnReady()
    {
        _spawnReady = true;
        if(_spawnRate > 1f)
        {
            _spawnRate -= 0.1f;
        }
        
    }
}
