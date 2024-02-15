using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    public float fallTime = 0.5f;
    private bool isSensed;
    [SerializeField] private Transform sensor;
    [SerializeField] private float sensorLength;
    [SerializeField] private LayerMask collisionTarget;

    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CollisionCheck();

        if (isSensed)
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void CollisionCheck()
    {
        isSensed = Physics2D.Raycast(sensor.position, Vector2.down, sensorLength, collisionTarget);
    }


}
