using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : GameUnit
{
    private Weapon _weapon;
    [SerializeField] private List<GameObject> _weapons;

    private void Start()
    {
        _weapon = (Weapon)Random.Range(0, 3);
        _weapons[(int)_weapon].SetActive(true);
    }

    public override float GetEnemySpeed()
    {
        return _speed;
    }
}