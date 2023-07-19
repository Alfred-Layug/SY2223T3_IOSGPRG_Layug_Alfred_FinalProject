using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : GameUnit
{
    private bool _enemyNearby;
    private Vector2 _randomPosition;
    private Weapon _weapon;
    [SerializeField] private List<GameObject> _weapons;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GameUnit>() != null)
        {
            StopCoroutine(ChangePosition());
            _enemyNearby = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<GameUnit>() != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, collision.gameObject.transform.position,
                    _speed * Time.deltaTime * 0.5f);
            transform.up = collision.gameObject.transform.position - transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<GameUnit>() != null)
        {
            _enemyNearby = false;
            Wander();
        }
    }

    private void Start()
    {
        _weapon = (Weapon)Random.Range(0, 3);
        if (_weapon == Weapon.Pistol)
        {
            _weapons[0].SetActive(true);
        }
        else if (_weapon == Weapon.AutomaticRifle)
        {
            _weapons[1].SetActive(true);
        }
        else if (_weapon == Weapon.Shotgun)
        {
            _weapons[2].SetActive(true);
        }
        Wander();
    }

    private void Update()
    {
        if (!_enemyNearby)
        {
            transform.Translate(_randomPosition * _speed * Time.deltaTime);
        }
    }

    public override void Wander()
    {
        base.Wander();
        _enemyNearby = false;
        _randomPosition = Random.insideUnitCircle;
        StartCoroutine(ChangePosition());
    }

    private IEnumerator ChangePosition()
    {
        while (!_enemyNearby)
        {
            yield return new WaitForSeconds(3f);
            _randomPosition = Random.insideUnitCircle;
        }
    }
}
