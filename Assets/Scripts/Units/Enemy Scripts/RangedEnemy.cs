using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : GameUnit
{
    private Weapon _weapon;
    [SerializeField] private List<GameObject> _weapons;
    [SerializeField] private List<Gun> _gunTypes;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _nozzle;

    private void Start()
    {
        _weapon = (Weapon)Random.Range(0, 3);
        _weapons[(int)_weapon].SetActive(true);
        _currentGun = _gunTypes[(int)_weapon];
        _currentGun._isEnemy = true;
        _health._healthBar.enabled = false;
        _health._healthBarBackground.enabled = false;
        _health._healthText.enabled = false;
    }

    public override void Shoot()
    {
        _currentGun.Shoot(_bulletPrefab, _nozzle);
    }

    public override void DoDeath()
    {
        Spawner.instance.DecreaseUnitCount(this);
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