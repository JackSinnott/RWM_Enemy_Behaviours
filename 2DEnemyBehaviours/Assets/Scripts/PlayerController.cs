using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    public float horizontalInput;
    public float xMovement;
    public float distance;
    public LayerMask groundLayer;


    RaycastHit2D raycastHit;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        xMovement = horizontalInput * speed;

        _rb.velocity = new Vector2(xMovement, _rb.velocity.y);
        
       
        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.velocity = Vector2.up * jumpForce;
                Debug.Log("Should be jumping");
            }
        }
    }

    private bool IsGrounded()
    {

        raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, distance, groundLayer);

        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + distance));
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + distance));
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y), Vector2.right * (boxCollider2D.bounds.extents.x) * 2);


        return raycastHit.collider != null;
    }

    public void Hit()
    {
        _rb.AddForce(new Vector2(-300, 3), ForceMode2D.Force);
        Debug.Log("HIT");
    }
}
