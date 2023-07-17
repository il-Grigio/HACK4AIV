using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MenuButton {
    public override void ExecuteButton() {
        Debug.Log("Game quit!");
        Application.Quit();
    }
}
