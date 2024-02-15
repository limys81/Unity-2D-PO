using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPregab;

    public void FireArrow()
    {
        GameObject arrow = Instantiate(arrowPregab, launchPoint.position, arrowPregab.transform.rotation);
        Vector3 origScale = arrow.transform.localScale;
        
        arrow.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x > 0 ? 0.5f : -0.5f, origScale.y, origScale.z);
    }
}
