using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    public float blockingTime = 2f;
    public float bloackingCoolDown = 1.5f;
    public float blockingCoolTime = 0;

    public bool isBlocking;
    public bool isCounterAttack = false;

    Animator anim;
    Damageable damageable;
    Skeleton skeleton;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        skeleton = GetComponent<Skeleton>();
    }

    // Update is called once per frame
    void Update()
    {
        IsBlocking();
    }

    private void FixedUpdate()
    {
        AnimatorController();
    }

    private void IsBlocking()
    {
        if (skeleton.HasTarget && !isBlocking)
        {
            blockingCoolTime += Time.deltaTime;
        }

        if (blockingCoolTime > 3)
        {
            isBlocking = true;
            blockingCoolTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBlocking)
        {
            isBlocking = false;
            isCounterAttack = true;
        }
    }

    private void AnimatorController()
    {
        anim.SetBool("isBlocking", isBlocking);
        anim.SetBool("isCounterAttack", isCounterAttack);
    }
}
