using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] Transform m_playerGO;
    [SerializeField] SphereCollider m_targetRadius;
    [SerializeField] private LayerMask m_playerMask;
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] float m_rayDistance;


    private RaycastHit2D m_targetRay;

    public float fireRate = 1f; // time between shots

    float fireCountdown = 0f;


    private void Start()
    {
        if (m_playerGO == null) m_playerGO = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (m_targetRadius == null) m_targetRadius = GetComponentInChildren<SphereCollider>();
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, m_targetRadius.radius, m_playerMask))
        {
            if (LineOfSightToTarget())
            {
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
        }

        Debug.DrawRay(transform.position, (m_playerGO.position - transform.position) * m_rayDistance, Color.white);
    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, m_targetRadius.radius);
    //}

    private void Shoot()
    {
        GameObject instance = Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);
        Bullet bullet = instance.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(m_playerGO);
        }
    }

    private bool LineOfSightToTarget()
    {

        m_targetRay = Physics2D.Linecast(transform.position, m_playerGO.position * m_rayDistance, m_playerMask);

        return m_targetRay.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
