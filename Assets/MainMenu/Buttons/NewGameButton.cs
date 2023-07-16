using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NewGameButton : MenuButton {
    [SerializeField] private PostProcessVolume ppCutscene;
    [SerializeField] private Lifter[] panelsToLiftUp;
    [SerializeField] private Transform selector;
    private bool startCutscene;
    [SerializeField] private float ppWeightSpeed;
    [SerializeField] private Animation cutsceneAnimation;

    private void Update() {
        if (startCutscene) {
            ppCutscene.weight = Mathf.Lerp(ppCutscene.weight, 1, ppWeightSpeed * Time.deltaTime);
        }
    }
    public override void ExecuteButton() {
        selector.gameObject.SetActive(false); //Temp
        startCutscene = true;
        cutsceneAnimation.Play();
        foreach (Lifter panel in panelsToLiftUp) { panel.LiftUp(); }
    }
}
