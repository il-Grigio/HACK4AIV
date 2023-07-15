using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifterButton : MenuButton {
    [SerializeField] private Lifter panelToLiftDown;
    [SerializeField] private Lifter[] panelsToLiftUp;
    public bool tabOpened;
    public override void ExecuteButton() {
        tabOpened = !tabOpened;
        if (tabOpened) { panelToLiftDown.LiftDown(); } else { panelToLiftDown.LiftUp(); }
        foreach(Lifter panel in panelsToLiftUp) { panel.LiftUp(); }
    }
}
