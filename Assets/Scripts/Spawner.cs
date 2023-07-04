using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _meleeEnemyPrefab;
    [SerializeField] private GameObject _rangedEnemyPrefab;
    [SerializeField] private GameObject _bossEnemyPrefab;
    [SerializeField] private List<GameUnit> _enemies;

    [SerializeField] private GameObject _pistolAmmoPrefab;
    [SerializeField] private GameObject _automaticRifleAmmoPrefab;
    [SerializeField] private GameObject _shotgunAmmoPrefab;
    [SerializeField] private List<Ammo> _ammoPickUps;

    private float _spawnCollisionCheckradius;

    private void Start()
    {
        _spawnCollisionCheckradius = 1;

        SpawnEnemies(5, _meleeEnemyPrefab, "Arthur Melee", 100, 5);
        SpawnEnemies(3, _rangedEnemyPrefab, "Arthur Ranged", 75, 7);
        SpawnEnemies(1, _bossEnemyPrefab, "Arthur Boss", 1000, 3);
        SpawnAmmo(3, _pistolAmmoPrefab, "Pistol Ammo");
        SpawnAmmo(3, _automaticRifleAmmoPrefab, "Automatic Rifle Ammo");
        SpawnAmmo(3, _shotgunAmmoPrefab, "Shotgun Ammo");
    }

    private void SpawnEnemies(int count, GameObject prefab, string name, int maxHealth, float speed)
    {
        float _randomX;
        float _randomY;
        Vector3 _randomPosition;

        for (int i = 0; i < count; i++)
        {
            _randomX = Random.Range(-95, 95);
            _randomY = Random.Range(-45, 45);
            _randomPosition = new Vector3(_randomX, _randomY, 0);

            if (!Physics2D.OverlapCircle(_randomPosition, _spawnCollisionCheckradius))
            {
                GameObject enemyGO = Instantiate(prefab, _randomPosition, Quaternion.identity);
                enemyGO.transform.parent = transform;

                GameUnit gameUnit = enemyGO.GetComponent<GameUnit>();
                _enemies.Add(gameUnit);

                gameUnit.Initialize(name, maxHealth, speed);
            }
            else
            {
                i--;
            }
        }
    }

    private void SpawnAmmo(int count, GameObject prefab, string ammoType)
    {
        float _randomX;
        float _randomY;
        Vector3 _randomPosition;

        for (int i = 0; i < count; i++)
        {
            _randomX = Random.Range(-95, 95);
            _randomY = Random.Range(-45, 45);
            _randomPosition = new Vector3(_randomX, _randomY, 0);

            if (!Physics2D.OverlapCircle(_randomPosition, _spawnCollisionCheckradius))
            {
                GameObject ammoGO = Instantiate(prefab, _randomPosition, Quaternion.identity);
                ammoGO.transform.parent = transform;

                Ammo ammo = ammoGO.GetComponent<Ammo>();
                _ammoPickUps.Add(ammo);

                ammo.Initialize(ammoType);
            }
            else
            {
                i--;
            }
        }
    }
}
