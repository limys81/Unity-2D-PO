using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    FadeInOut fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GoToGamePlayScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("GamePlayScene");
    }

    public IEnumerator GoToMainMenuScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickGameStartBtn()
    {
        StartCoroutine(GoToGamePlayScene());
    }

    public void OnClickOptionBtn()
    {

    }
  
    public void OnClickControllerBtn()
    {

    }

    public void OnClickGoToMainMenuBtn()
    {
        StartCoroutine(GoToMainMenuScene());
    }

    public void OnClickQuitAgreeBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

