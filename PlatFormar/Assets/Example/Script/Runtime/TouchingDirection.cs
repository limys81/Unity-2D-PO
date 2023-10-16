using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float WallDistance = 0.3f;
    
    CapsuleCollider2D touchingCol;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] WallHits = new RaycastHit2D[10];

    [SerializeField] private bool _isGrounded;

    public bool IsGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
    }

    [SerializeField] private bool _isWallDitected;

    public bool IsWallDitected
    {
        get { return _isWallDitected; }
        private set
        {
            _isWallDitected = value;
            animator.SetBool("isWallDitected", value);
        }
    }

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsWallDitected = touchingCol.Cast(wallCheckDirection, castFilter, WallHits, WallDistance) > 0;
    }
}
