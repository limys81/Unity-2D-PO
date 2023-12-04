using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiamondManager : MonoBehaviour
{
    public int diamondCount;
    public int maxDiamondCount = 5;
    public TMP_Text diamondText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        diamondText.text = diamondCount.ToString() + " / " + maxDiamondCount.ToString();
    }
}
