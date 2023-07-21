using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool _enemyNearby;
    private int _enemiesDetected;
    private Vector2 _randomPosition;
    private GameUnit _gameUnitScript;
    private GameObject _enemyDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GameUnit>() != null)
        {
            StopCoroutine(ChangePosition());
            _enemyDetected = collision.gameObject;
            _enemyNearby = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<GameUnit>() != null && _enemiesDetected == 0)
        {
            _enemiesDetected++;
            _enemyDetected = collision.gameObject;
        }

        if (collision.GetComponent<GameUnit>() != null && _enemyDetected != null)
        {
            if (Vector3.Distance(transform.position, _enemyDetected.transform.position) > 4)
            {
                transform.position = Vector2.MoveTowards(transform.position, _enemyDetected.transform.position,
                        _gameUnitScript.GetEnemySpeed() * Time.deltaTime * 0.25f);
            }
            transform.up = _enemyDetected.transform.position - transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<GameUnit>() != null)
        {
            _enemiesDetected--;
            _enemyDetected = null;
            _enemyNearby = false;
            Wander();
        }
    }

    private void Start()
    {
        _enemiesDetected = 0;
        _gameUnitScript = this.GetComponent<GameUnit>();
        Wander();
    }

    private void Update()
    {
        if (!_enemyNearby)
        {
            transform.Translate(_randomPosition * _gameUnitScript.GetEnemySpeed() * Time.deltaTime);
        }
    }

    public void Wander()
    {
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
