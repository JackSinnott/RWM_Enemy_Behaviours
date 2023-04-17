using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] PatrolEnemy _patrolEnemy;
    [SerializeField] GameObject _player;
    Vector2 _playerDirection;
    bool spawned = false;

    private void Start()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _playerDirection = _player.transform.position - transform.position;

        SummonEnemy();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Patrol"))
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void SummonEnemy()
    {
        if (!spawned)
        {
            var enemy = Instantiate(_patrolEnemy, transform.position, Quaternion.identity).GetComponent<PatrolEnemy>();
            spawned = true;
            if (_playerDirection.x > 0)
            {
                enemy.FlipSprite();
            }
        }
    }
}
