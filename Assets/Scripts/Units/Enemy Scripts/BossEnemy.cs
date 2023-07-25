using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : GameUnit
{
    [SerializeField] private GameObject _rocketPrefab;
    [SerializeField] private GameObject _nozzle;
    [SerializeField] private GameObject _rocketLauncherDropPrefab;

    public override void Initialize(string name, int maxHealth, float speed)
    {
        Debug.Log("Warning! Boss is spawning!");
        base.Initialize(name, maxHealth, speed);
        Debug.Log("Boss has spawned!");
    }

    public override void Shoot()
    {
        _currentGun.EnemyShoot(_rocketPrefab, _nozzle);
    }

    public override void DoDeath()
    {
        Spawner.instance.DecreaseUnitCount(this);
        Instantiate(_rocketLauncherDropPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
