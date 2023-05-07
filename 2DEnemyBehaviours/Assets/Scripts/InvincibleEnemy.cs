using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleEnemy : MonoBehaviour
{
    private Vector3 _pos;

    public float _speed;
    [SerializeField] private float _frequency;
    [SerializeField] private float _magnitude;

    [SerializeField] private float _maxBounds;
    [SerializeField] private float _minBounds;

    private void Start()
    {
        _pos = new Vector3(12,Random.Range(_minBounds, _maxBounds),0);
        transform.position = _pos;
    }

    private void Update()
    {
        MoveLeft();
        if (_pos.x < -12)
        {
            this.gameObject.SetActive(false);
        }
    }

    void MoveLeft()
    {
        _pos -= transform.right * Time.deltaTime * _speed;
        transform.position = _pos + transform.up * Mathf.Sin(Time.time * _frequency) * _magnitude;
    }

}
