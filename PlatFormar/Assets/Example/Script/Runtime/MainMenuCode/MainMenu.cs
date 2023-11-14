using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGameStartBtn()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void OnClickOptionBtn()
    {

    }

    public void OnClickControllerBtn()
    {

    }

    public void OnClickQuitBtn()
    {
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}

