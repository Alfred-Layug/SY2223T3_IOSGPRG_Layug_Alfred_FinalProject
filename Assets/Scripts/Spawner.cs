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
    [SerializeField] private List<AmmoPickup> _ammoPickUps;

    private float _spawnCollisionCheckradius;

    private void Start()
    {
        _spawnCollisionCheckradius = 1;

        SpawnEnemies(5, _meleeEnemyPrefab, "Arthur Melee", 100, 5);
        SpawnEnemies(3, _rangedEnemyPrefab, "Arthur Ranged", 75, 7);
        SpawnEnemies(1, _bossEnemyPrefab, "Arthur Boss", 1000, 3);
        SpawnAmmo(5, _pistolAmmoPrefab, AmmoType.PistolAmmo);
        SpawnAmmo(5, _automaticRifleAmmoPrefab, AmmoType.AutomaticRifleAmmo);
        SpawnAmmo(5, _shotgunAmmoPrefab, AmmoType.ShotgunAmmo);
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

    private void SpawnAmmo(int count, GameObject prefab, AmmoType ammoType)
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

                AmmoPickup ammoPickup = ammoGO.GetComponent<AmmoPickup>();
                _ammoPickUps.Add(ammoPickup);

                ammoPickup.Initialize(ammoType);
            }
            else
            {
                i--;
            }
        }
    }
}
