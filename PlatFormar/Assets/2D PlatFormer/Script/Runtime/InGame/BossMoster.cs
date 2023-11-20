using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]

public class BossMoster : MonoBehaviour
{
    public float movePower;
    public float walkStopRate;
    int movementFlag = 0;

    public bool isWalking;
    public bool trackingState = false;

    public GameObject spellAttackZonePrefab;
    public bool isSpellAttack;

    float spellAttackCoolTime = 0;
    float spellAttackCoolDown = 1.5f;
    float spellAttackTime = 0.25f;

    public GameObject bossHealthBar;

    private Transform player;

    public DetectionZone attackZone;
    public DetectionZone MoveZone;

    Rigidbody2D rigid;
    TouchingDirection touchingDirection;
    Animator anim;
    Damageable damageable;

    public bool _hasAttackTarget = false;
    public bool _hasMoveTarget = false;

    public bool HasAttackTarget
    {
        get { return _hasAttackTarget; }
        private set
        {
            _hasAttackTarget = value;
            anim.SetBool("hasAttackTarget", value);
        }
    }

    public bool HasMoveTarget
    {
        get { return _hasMoveTarget; }
        private set
        {
            _hasMoveTarget = value;
            anim.SetBool("hasMoveTarget", value);
        }
    }

    public bool CanMove
    {
        get
        {
            return anim.GetBool("canMove");
        }
    }

    public float AttackCooldown
    {
        get
        {
            return anim.GetFloat("attackCooldown");

        }
        private set
        {
            anim.SetFloat("attackCooldown", Mathf.Max(value, 0));
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        HasAttackTarget = attackZone.detectedColliders.Count > 0;

        HasMoveTarget = MoveZone.detectedColliders.Count > 0;

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        if (CanMove && trackingState)
        {
            spellAttackCoolTime += Time.deltaTime;
        }

        if (spellAttackCoolTime > 10)
        {
            StartCoroutine(SpellAttack());
            spellAttackCoolTime = 0;
        }
        if (trackingState)
        {
            bossHealthBar.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        AnimatorController();

        Move();
        FlipDirection();
    }

    public void OnHit(int damage)
    {
        rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y + rigid.velocity.y);
    }

    public void Move()
    {
        if (CanMove && _hasMoveTarget)
        {
            trackingState = true;
        }
        else
        {
            rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, 0, walkStopRate), rigid.velocity.y);
        }

        if (CanMove && trackingState && !_hasAttackTarget && !isSpellAttack)
        {
            isWalking = true;
        }
        else if (CanMove && _hasAttackTarget)
        {
            isWalking = false;
        }
    }

    private void FlipDirection()
    {
        Vector3 moveVelocity = player.position - transform.position;
        moveVelocity.Normalize();
        string dist = "";

        if (trackingState && isWalking)
        {
            transform.position += moveVelocity * movePower * Time.deltaTime;    

            Vector3 playerPos = player.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x > transform.position.x)
                dist = "Right";
        }
        else
        {
            if (movementFlag == 1)
                dist = "Left";
            else if (movementFlag == 2)
                dist = "Right";
        }

        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(2, 2, 2);
        }
        else if (dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-2, 2, 2);
        };
    }

    public IEnumerator SpellAttack()
    {
        if (CanMove)
        {
            isWalking = false;
            isSpellAttack = true;
            yield return new WaitForSeconds(spellAttackTime);

            isSpellAttack = false;
            yield return new WaitForSeconds(spellAttackCoolDown);

            for(int i = 0; i < 5; i++)
            {
                GameObject spellAttackZone = Instantiate(spellAttackZonePrefab, transform.position, transform.rotation);
                spellAttackZone.transform.position = new Vector3((player.transform.position.x - 14) + 7 * i, -5, 0);
            }
        }
    }

    private void AnimatorController()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isSpellAttack", isSpellAttack);
        anim.SetBool("trackingState", trackingState);
    }
}
