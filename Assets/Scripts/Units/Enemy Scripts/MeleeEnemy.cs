using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : GameUnit
{
    private bool _enemyNearby;
    private Vector2 _randomPosition;

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
        }
    }

    private void Update()
    {
        if (!_enemyNearby)
        {
            transform.Translate(_randomPosition * _speed * Time.deltaTime);
        }
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
