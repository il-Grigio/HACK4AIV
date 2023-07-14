using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifterButton : MenuButton {
    [SerializeField] private Lifter panelToLiftUp;
    [SerializeField] private Lifter[] panelsToLiftDown;
    public bool tabOpened;
    public override void ExecuteButton() {
        tabOpened = !tabOpened;
        if (tabOpened) { panelToLiftUp.LiftDown(); } else { panelToLiftUp.LiftUp(); }
        foreach(Lifter panel in panelsToLiftDown) { panel.LiftUp(); }
    }
}
