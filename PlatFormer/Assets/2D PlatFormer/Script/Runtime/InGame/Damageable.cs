using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    public UnityEvent<int, int> healthChanged;

    Animator anim;

    [SerializeField] public int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        } 
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField] private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField] private bool _isAlive = true;
    [SerializeField] public bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            anim.SetBool("isAlive", value);
            Debug.Log("IsAlive set " + value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return anim.GetBool("lockVelocity");
        }
        set
        {
            anim.SetBool("lockVelocity", value);
        }
    }

    public bool IsInvincible
    {
        get
        {
            return anim.GetBool("isInvincible");
        }
        set
        {
            anim.SetBool("isInvincible", value);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            anim.SetTrigger("hit");
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);

            return true;
        }

        return false;
    }
} 
