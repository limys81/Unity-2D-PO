using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public GameObject buffTextPrefeb;

    public Canvas gameCanvas;
    public Player player;

    public float buffTime = 300f;
    public int buffValue = 5;
    public bool onbuff;
    
    private void Awake()
    {

    }

    private void Update()
    {
        if (DiamondScore.instance.currentScore == DiamondScore.instance.maxDiamondCount)
        {
            onbuff = true;
            buffTime -= Time.deltaTime;
        }
        else
        {
            onbuff = false;
            buffTime = 300f;
        }
    }

    private void OnBuff()
    {
        if (onbuff)
        {
            Attack.Instance.attackDamage += buffValue;
        }
    }
}
