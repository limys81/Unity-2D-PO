using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class DiamondManager : MonoBehaviour
{
    public int curdiamondCount;
    public int maxDiamondCount = 5;
    public TMP_Text diamondText;

    public int SaveDiamondCount;

    void Awake()
    {
        SaveDiamondCount = PlayerPrefs.GetInt("SaveDiamond");
        PlayerPrefs.SetInt("SaveDiamond", curdiamondCount);
    }

    // Update is called once per frame
    void Update()
    {
        diamondText.text = curdiamondCount.ToString() + " / " + maxDiamondCount.ToString();
    }
}
