using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiamondScore : MonoBehaviour
{
    public static DiamondScore instance;

    public TMP_Text diamondText;


    int diamondSore = 0;
    int currentScore = 0;
    int maxDiamondCount = 5;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentScore = PlayerPrefs.GetInt("currentScore", 0);
        diamondText.text = currentScore.ToString() + " / " + maxDiamondCount.ToString();
    }

    public void AddPoint()
    {
        diamondSore += 1;
        diamondText.text = diamondSore.ToString() + " / " + maxDiamondCount.ToString();
        PlayerPrefs.SetInt("currentScore", diamondSore);
    }
}
