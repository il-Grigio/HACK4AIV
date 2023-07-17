using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float showingTime = 2;
    float currentTime;

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0) {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0) {
                textMeshProUGUI.text = "";
            }
        }
    }

    public void SetText() {
        if(ClientOrderMGR.Instance.CurrentPhaseIndex > 0) {
            currentTime = showingTime;
            textMeshProUGUI.text = "Phase " + (ClientOrderMGR.Instance.CurrentPhaseIndex + 1) + " out of 5";
        }
    }
}
