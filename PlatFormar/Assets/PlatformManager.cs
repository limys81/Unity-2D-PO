using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;
    [SerializeField] GameObject platform;
    [SerializeField] float posX1, posX2, posX3, posY1, posY2, posY3;
    [SerializeField] float spawnTime = 5f;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        Instantiate(platform, new Vector2(posX1, posY1), platform.transform.rotation);
        Instantiate(platform, new Vector2(posX2, posY2), platform.transform.rotation);
        Instantiate(platform, new Vector2(posX3, posY3), platform.transform.rotation);
    }

    IEnumerator spawnPlatform(Vector2 spawnPos)
    {
        yield return new WaitForSeconds(spawnTime);
        Instantiate(platform, spawnPos, platform.transform.rotation);
    }
}
