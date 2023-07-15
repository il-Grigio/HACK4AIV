using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

public class UIGeneralTimer : MonoBehaviour
{
    [SerializeField] private RectTransform barSlider;
    [HideInInspector] public float time;
    public TMP_Text text;

    private float timeMultiplier;
    private float currentTime;
    

    // Start is called before the first frame update
    void Start()
    {
        currentTime = time;
        barSlider.GetComponent<Animator>().SetFloat("Time", 1f / time);
    }

    public void SetTimer(float newTime)
    {
        time = newTime;
        currentTime = newTime;
        barSlider.GetComponent<Animator>().SetFloat("Time", 1f / time);
        barSlider.GetComponent<Animator>().Play("Timer", 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime >= 0)
        {
            text.text = "" + (int)(currentTime / 60) + ":" + (currentTime % 60 < 10 ? "0" : "") + (int)currentTime % 60;
        }
    }
}
