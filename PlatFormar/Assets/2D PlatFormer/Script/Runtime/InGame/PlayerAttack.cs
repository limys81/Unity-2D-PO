using EthanTheHero;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region 변수
    private Animator anim;

    [Header("Player Attack Value")]
    public int attackCount = 0;
    float lastClickedTime = 0;
    public float maxComboDelay;

    Player player;
    #endregion // 변수

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        OnAttack();
    }

    public void OnAttack()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
            attackCount = 0;

        if (Input.GetMouseButtonDown(0) && !player.isRunning && !player.isDashing)
        {
            lastClickedTime = Time.time;
            attackCount++;

            if(attackCount == 1)
            {
                anim.SetBool("Attack01", true);
            }
            attackCount = Mathf.Clamp(attackCount, 0, 3);
        }
    }

    private void OnAttack01()
    {
        if (attackCount >= 2)
        {
            anim.SetBool("Attack02", true);
        }
        else
        {
            anim.SetBool("Attack01", false);
            attackCount = 0;
        }
    }

    private void OnAttack02()
    {
        if (attackCount >= 3)
        {
            anim.SetBool("Attack03", true);
        }
        else
        {
            anim.SetBool("Attack02", false);
            attackCount = 0;
        }
    }

    private void OnAttack03()
    {
        anim.SetBool("Attack01", false);
        anim.SetBool("Attack02", false);
        anim.SetBool("Attack03", false);

        attackCount = 0;
    }
}