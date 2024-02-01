using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutSceneControl : MonoBehaviour
{
    public GameObject backGroundMusic;
    public GameObject bossRoomSound;

    public PlayableDirector pd;
    public TimelineAsset[] ta;

    void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CutScene")
        {
            other.gameObject.SetActive(false);
            pd.Play(ta[0]);

            StartCoroutine(WaitForIt());       
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(1.5f);
        backGroundMusic.SetActive(false);
        bossRoomSound.SetActive(true);
    }
}
