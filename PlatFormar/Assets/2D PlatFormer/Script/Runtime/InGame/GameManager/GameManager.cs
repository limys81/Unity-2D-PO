using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Control")]
    public bool isGameOver = false;
    public bool isPause = false;

    [Header("UI")]
    public GameObject fadeImg;
    public GameObject setUpUI;
    public GameObject GameOverUI;
    Vector2 creatPoint;

    private void Start()
    {
        instance = this;
        creatPoint = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    private void Awake()
    {
        
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        GameOverUI.SetActive(true);
        fadeImg.SetActive(true);
        IsPause();
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
