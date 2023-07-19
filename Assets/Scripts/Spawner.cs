using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [SerializeField] private GameObject _meleeEnemyPrefab;
    [SerializeField] private GameObject _rangedEnemyPrefab;
    [SerializeField] private GameObject _bossEnemyPrefab;
    [SerializeField] private List<GameUnit> _enemies;

    [SerializeField] private GameObject _pistolAmmoPrefab;
    [SerializeField] private GameObject _automaticRifleAmmoPrefab;
    [SerializeField] private GameObject _shotgunAmmoPrefab;
    [SerializeField] private List<AmmoPickup> _ammoPickUps;

    [SerializeField] private GameObject _pistolLootPrefab;
    [SerializeField] private GameObject _automaticRifleLootPrefab;
    [SerializeField] private GameObject _shotgunLootPrefab;
    [SerializeField] private List<WeaponPickup> _weaponPickUps;

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

        SpawnEnemies(7, _meleeEnemyPrefab, "Arthur Melee", 100, 4);
        SpawnEnemies(7, _rangedEnemyPrefab, "Arthur Ranged", 75, 6);
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

    private void SpawnWeapon(int count, GameObject prefab, Weapon weapon)
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
                GameObject weaponGO = Instantiate(prefab, _randomPosition, Quaternion.identity);
                weaponGO.transform.parent = transform;

                WeaponPickup weaponPickup = weaponGO.GetComponent<WeaponPickup>();
                _weaponPickUps.Add(weaponPickup);

                weaponPickup.Initialize(weapon);
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

        for (int i = 0; i < count; i++)
        {
            pickUpChance = Random.Range(1f, 100f);
            
            if (pickUpChance > 30f)
            {
                AmmoType ammoType = (AmmoType)Random.Range(0, 3);

                if (ammoType == AmmoType.PistolAmmo)
                {
                    SpawnAmmo(1, _pistolAmmoPrefab, ammoType);
                }
                else if (ammoType == AmmoType.AutomaticRifleAmmo)
                {
                    SpawnAmmo(1, _automaticRifleAmmoPrefab, ammoType);
                }
                else if (ammoType == AmmoType.ShotgunAmmo)
                {
                    SpawnAmmo(1, _shotgunAmmoPrefab, ammoType);
                }
            }
            else
            {
                Weapon weapon = (Weapon)Random.Range(0, 3);
                
                if (weapon == Weapon.Pistol)
                {
                    SpawnWeapon(1, _pistolLootPrefab, weapon);
                }
                else if (weapon == Weapon.AutomaticRifle)
                {
                    SpawnWeapon(1, _automaticRifleLootPrefab, weapon);
                }
                else if (weapon == Weapon.Shotgun)
                {
                    SpawnWeapon(1, _shotgunLootPrefab, weapon);
                }
            }
        }
    }

    public void RemoveAmmoPickupFromList(AmmoPickup ammoPickup)
    {
        _ammoPickUps.Remove(ammoPickup);
    }

    public void RemoveWeaponPickupFromList(WeaponPickup weaponPickup)
    {
        _weaponPickUps.Remove(weaponPickup);
    }

    public void RemoveUnitFromList(GameUnit gameUnit)
    {
        _enemies.Remove(gameUnit);
    }
}
