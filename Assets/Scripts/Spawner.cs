using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [SerializeField] private GameObject _rangedEnemyPrefab;
    [SerializeField] private GameObject _bossEnemyPrefab;
    [SerializeField] private List<GameUnit> _enemies;

    [SerializeField] private List<GameObject> _ammoPickupPrefabs;
    [SerializeField] private List<GameObject> _weaponPickupPrefabs;
    [SerializeField] private List<Pickup> _pickups;

    private float _spawnCollisionCheckradius;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _spawnCollisionCheckradius = 1;
        SpawnEnemies(15, _rangedEnemyPrefab, "Arthur Ranged", 100, 6);
        SpawnEnemies(1, _bossEnemyPrefab, "Arthur Boss", 1000, 2);
        SpawnPickups(50);
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

    private void SpawnPickups(int count)
    {
        float pickUpChance;
        float _randomX;
        float _randomY;
        Vector3 _randomPosition;

        for (int i = 0; i < count; i++)
        {
            pickUpChance = Random.Range(1f, 100f);
            _randomX = Random.Range(-95, 95);
            _randomY = Random.Range(-45, 45);
            _randomPosition = new Vector3(_randomX, _randomY, 0);
            Weapon weapon = (Weapon)Random.Range(0, 3);

            if (pickUpChance > 30f)
            {
                if (!Physics2D.OverlapCircle(_randomPosition, _spawnCollisionCheckradius))
                {
                    GameObject pickupGO = Instantiate(_ammoPickupPrefabs[(int)weapon], _randomPosition, Quaternion.identity);
                    pickupGO.transform.parent = transform;

                    Pickup pickup = pickupGO.GetComponent<Pickup>();
                    _pickups.Add(pickup);

                    pickup.Initialize(weapon);
                }
                else
                {
                    i--;
                }
            }
            else
            {
                if (!Physics2D.OverlapCircle(_randomPosition, _spawnCollisionCheckradius))
                {
                    GameObject pickupGO = Instantiate(_weaponPickupPrefabs[(int)weapon], _randomPosition, Quaternion.identity);
                    pickupGO.transform.parent = transform;

                    Pickup pickup = pickupGO.GetComponent<Pickup>();
                    _pickups.Add(pickup);

                    pickup.Initialize(weapon);
                    pickup._isWeaponPickup = true;
                }
                else
                {
                    i--;
                }
            }
        }
    }

    public void RemovePickupFromList(Pickup pickup)
    {
        _pickups.Remove(pickup);
    }

    public void RemoveUnitFromList(GameUnit gameUnit)
    {
        _enemies.Remove(gameUnit);
    }
}