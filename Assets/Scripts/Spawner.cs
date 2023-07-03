using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _meleeEnemyPrefab;
    [SerializeField] private GameObject _rangedEnemyPrefab;
    [SerializeField] private GameObject _bossEnemyPrefab;
    [SerializeField] private List<GameUnit> _enemies;

    private void Start()
    {
        SpawnEnemies(5, _meleeEnemyPrefab, "Arthur Melee", 100, 5);
        SpawnEnemies(3, _rangedEnemyPrefab, "Arthur Ranged", 75, 7);
        SpawnEnemies(1, _bossEnemyPrefab, "Arthur Boss", 1000, 3);
    }

    private void SpawnEnemies(int count, GameObject prefab, string name, int maxHealth, float speed)
    {
        float _randomX;
        float _randomY;
        Vector3 _randomPosition = new Vector3();

        for (int i = 0; i < count; i++)
        {
            _randomX = Random.Range(-5, 5);
            _randomY = Random.Range(-5, 5);
            _randomPosition = new Vector3(_randomX, _randomY, 0);

            GameObject enemyGO = Instantiate(prefab, _randomPosition, Quaternion.identity);
            enemyGO.transform.parent = transform;

            GameUnit gameUnit = enemyGO.GetComponent<GameUnit>();
            _enemies.Add(gameUnit);

            gameUnit.Initialize(name, maxHealth, speed);
        }
    }
}
