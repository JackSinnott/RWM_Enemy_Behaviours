using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private int _speed = 0;
    [SerializeField] private int _maxSpeed = 5;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _rayDistance = 0.05f;
    [SerializeField] GameObject _player;

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spRenderer;
    private RaycastHit2D _endOfPlatformRay;
    private bool _movingLeft = true;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _boxCollider = this.GetComponent<BoxCollider2D>();
        _spRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (EndOfPlatform())
        {
            Move();
        }
        else
        {
            FlipSprite();
        }
    }

    private void Move()
    {
        if (Mathf.Abs(_rb.velocity.x) <= _maxSpeed) // If our velocity desplayed as a positive is <= maxSpeed
            _rb.velocity += new Vector2(_speed, 0) * Time.deltaTime;
 
    }

    private bool EndOfPlatform()
    {
        if(_movingLeft)
            _endOfPlatformRay = Physics2D.Linecast(_boxCollider.bounds.min, _boxCollider.bounds.min - new Vector3(-_rayDistance, -_rayDistance), _groundMask);
        else
            _endOfPlatformRay = Physics2D.Linecast(_boxCollider.bounds.min + new Vector3(_boxCollider.bounds.extents.x * 2, 0f, 0f),
                (_boxCollider.bounds.min + new Vector3(_boxCollider.bounds.extents.x * 2, 0f, 0f)) - new Vector3(_rayDistance, -_rayDistance, 0f), _groundMask);

        return _endOfPlatformRay.collider != null;
    }

    private void FlipSprite()
    {
        _spRenderer.flipX = !_spRenderer.flipX;
        _rb.velocity = new Vector2(0f, 0f);
        _movingLeft = !_movingLeft;
        _speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        { 
            _player.GetComponent<PlayerController>().Hit();
        }
    }
}
