using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProgressBar : MonoBehaviour
{
    [SerializeField] private Transform bar;
    
    public float Progress
    {
        get
        {
            return progress;
        }
        set
        {
            progress = Mathf.Clamp01(value);
            bar.localScale = new Vector3(progress, 1, 1);
        }
    }

    private float progress;
}
