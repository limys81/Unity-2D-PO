using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    [SerializeField] float fallTime = 0.5f, destroyTime = 0.5f;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlatformManager.Instance.StartCoroutine("spawnPlatform",
                new Vector2(transform.position.x, transform.position.y));
            Invoke("FallPlatform", fallTime);
            Destroy(gameObject, destroyTime);
        }
    }

    void FallPlatform()
    {
        rigid.isKinematic = false;
    }
}