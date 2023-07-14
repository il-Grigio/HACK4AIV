using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MenuButton {
    [SerializeField] private Lifter settingsPanelLift;
    private bool tabOpened;
    public override void ExecuteButton() {
        tabOpened = !tabOpened;
        if (tabOpened) { settingsPanelLift.LiftDown(); } else { settingsPanelLift.LiftUp(); }
    }
}
