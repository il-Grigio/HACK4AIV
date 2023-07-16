using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGeneralTimer : MonoBehaviour
{
    [SerializeField] private RectTransform barSlider;
    [SerializeField] Color green = Color.green;
    [SerializeField] Color yellow = Color.yellow;
    [SerializeField] Color red = Color.red;
    [SerializeField] Image barImage;

    [HideInInspector] public float time = 100;
    public TMP_Text text;

    private float timeMultiplier;
    private float currentTime;
    float startingSize;    

    // Start is called before the first frame update
    void Start()
    {
        time = 100;
        currentTime = time;
        startingSize = barSlider.localScale.x;
        //barSlider.GetComponent<Animator>().SetFloat("Time", 1f / time);
    }

    public void SetTimer(float newTime, float maxTimer)
    {
        time = maxTimer;
        currentTime = newTime;
        //barSlider.GetComponent<Animator>().SetFloat("Time", /*1f /*/ (currentTime / time));
        //barSlider.GetComponent<Animator>().Play("Timer", 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        float percent = Mathf.Clamp01(currentTime / time);

        if(percent >= 0.5f) {
            barImage.color = Color.Lerp(yellow, green, percent * 2 - 1);
        }else
            barImage.color = Color.Lerp(red, yellow, percent * 2);

        barSlider.localScale = new Vector3(startingSize * percent, barSlider.localScale.y, barSlider.localScale.z);
        if (currentTime >= 0)
        {
            text.text = "" + (int)(currentTime / 60) + ":" + (currentTime % 60 < 10 ? "0" : "") + (int)currentTime % 60;
        }
    }
}
