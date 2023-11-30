using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageScene : MonoBehaviour
{
    public GameObject moveUI;

    FadeInOut fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickEsc();
        }
    }

    public IEnumerator GoToGamePlayScene01()
    {
        fade.FadeIn();
        yield return new WaitForSecondsRealtime(2);

        SceneManager.LoadScene("GamePlayScene01");
    }

    public IEnumerator GoToGamePlayScene02()
    {
        fade.FadeIn();
        yield return new WaitForSecondsRealtime(2);

        SceneManager.LoadScene("GamePlayScene02");
    }

    public IEnumerator GoToGamePlayScene03()
    {
        fade.FadeIn();
        yield return new WaitForSecondsRealtime(2);

        SceneManager.LoadScene("GamePlayScene03");
    }

    public void OnClickStage01()
    {
        StartCoroutine(GoToGamePlayScene01());
    }

    public void OnClickStage02()
    {
        StartCoroutine(GoToGamePlayScene02());
    }

    public void OnClickStage03()
    {
        StartCoroutine(GoToGamePlayScene03());
    }

    public void OnClickLeftButton()
    {
        if(moveUI.transform.position.x != 0)
        { 
            moveUI.transform.position = new Vector2(moveUI.transform.position.x + 300, moveUI.transform.position.y);
        }
    }

    public void OnClickRightButton()
    {
        moveUI.transform.position = new Vector2(moveUI.transform.position.x - 300, moveUI.transform.position.y);
    }

    public void OnClickEsc()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
