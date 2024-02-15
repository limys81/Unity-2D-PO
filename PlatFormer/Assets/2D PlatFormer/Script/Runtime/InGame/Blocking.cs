using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Blocking : MonoBehaviour
{
    public float blockingTime = 2f;
    public float bloackingCoolDown = 1.5f;
    public float blockingCoolTime = 0;

    public bool isBlocking;

    public GameObject blockTextPrefab;

    Animator anim;
    Skeleton skeleton;
    Damageable damageable;
    public Canvas gameCanvas;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        skeleton = GetComponent<Skeleton>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skeleton.HasTarget && !isBlocking)
        {
            blockingCoolTime += Time.deltaTime;
        }

        if (blockingCoolTime > 4)
        {
            StartCoroutine(Block());
            blockingCoolTime = 0;
        }
    }

    private void FixedUpdate()
    {
        AnimatorController();
    }

    public IEnumerator Block()
    {
        isBlocking = true;
        damageable.isInvincible = true;
        damageable.invincibilityTime = blockingTime;
        yield return new WaitForSeconds(blockingTime);

        isBlocking = false;
        damageable.invincibilityTime = 0.25f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBlocking)
        {
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            Instantiate(blockTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform);
            isBlocking = false;
            anim.SetTrigger("isCounterAttack");
        }
    }

    private void AnimatorController()
    {
        anim.SetBool("isBlocking", isBlocking);
    }
}
