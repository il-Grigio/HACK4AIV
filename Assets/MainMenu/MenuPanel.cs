using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private Transform[] lineStartAnchorPoints;
    [SerializeField] private Transform[] lineEndAnchorPoints;
    [SerializeField] private LineRenderer[] lines;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < lineStartAnchorPoints.Length; i++) {
            lines[i].SetPosition(0, lineStartAnchorPoints[i].position);
            lines[i].SetPosition(1, lineEndAnchorPoints[i].position);
        }
    }
}
