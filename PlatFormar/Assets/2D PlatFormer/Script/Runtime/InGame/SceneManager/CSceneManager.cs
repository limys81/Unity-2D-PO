using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager : MonoBehaviour
{
    public bool isPause = false;

    public GameObject fadeImg;
    public GameObject setUpUI;
    Vector2 creatPoint;

    private void Start()
    {
        creatPoint = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPause();

            if (setUpUI.activeSelf)
            {
                setUpUI.SetActive(false);
            }
            else
            {
                setUpUI.SetActive(true);
            }
        }

        fadeImg.SetActive(true);
    }

    public void IsPause()
    {
        isPause = !isPause;

        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        Time.fixedDeltaTime = 0.01f * Time.timeScale;
    }
}
