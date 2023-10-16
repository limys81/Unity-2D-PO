using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]

public class Skeleton : MonoBehaviour
{
    public float speed;
    public float walkStopRate;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rigid;
    TouchingDirection touchingDirection;
    Animator anim;
    Damageable damageable;

    public enum WalkableDirection { Right, Left };

    public WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value; }
    }

    public bool _hasTarget = false;

    public bool HasTarget { get { return _hasTarget; } private set
        {
            _hasTarget = value;
            anim.SetBool("hasTarget", value);
        }
    }

    public bool CanMove
    {
        get
        {
            return anim.GetBool("canMove");
        }
    }

    public float AttackCooldown { get {
            return anim.GetFloat("attackCooldown");
        } private set
        {
            anim.SetFloat("attackCooldown", Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirection.IsGrounded && touchingDirection.IsWallDitected)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove && !_hasTarget)
            {
                rigid.velocity = new Vector2(speed * walkDirectionVector.x, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, 0, walkStopRate), rigid.velocity.y);
            }
        } 
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        } else
        {
            Debug.LogError("ASD");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rigid.velocity = new Vector2(knockback.x, rigid.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirection.IsGrounded)
        {
            FlipDirection();
        }
    }
}
