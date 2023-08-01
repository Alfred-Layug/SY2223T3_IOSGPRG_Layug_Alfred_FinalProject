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

    private void Start()
    {
        _currentGun._isEnemy = true;
        _health._healthBar.enabled = false;
        _health._healthBarBackground.enabled = false;
        _health._healthText.enabled = false;
    }

    public override void Shoot()
    {
        _currentGun.Shoot(_rocketPrefab, _nozzle);
    }

    public override void DoDeath()
    {
        Spawner.instance.DecreaseUnitCount(this);
        Instantiate(_rocketLauncherDropPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override IEnumerator ShowEnemyHealthBar()
    {
        _health._healthBar.enabled = true;
        _health._healthBarBackground.enabled = true;
        _health._healthText.enabled = true;
        yield return new WaitForSeconds(5);
        _health._healthBar.enabled = false;
        _health._healthBarBackground.enabled = false;
        _health._healthText.enabled = false;
    }
}
